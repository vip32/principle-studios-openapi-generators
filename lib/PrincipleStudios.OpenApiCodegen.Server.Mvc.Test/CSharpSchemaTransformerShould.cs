﻿using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using PrincipleStudios.OpenApi.CSharp;
using PrincipleStudios.OpenApi.Transformations;
using Snapshooter.Xunit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static PrincipleStudios.OpenApiCodegen.TestUtils.DocumentHelpers;

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc
{
    using static OptionsHelpers;

    public class CSharpSchemaTransformerShould
    {
        public delegate OpenApiSchema SchemaAccessor(OpenApiDocument document);
        [Theory]
        [InlineData(false, "petstore.yaml", "paths./pets.get.parameters[?(@.name=='tags')].schema")]
        [InlineData(false, "petstore.yaml", "paths./pets.get.parameters[?(@.name=='limit')].schema")]
        [InlineData(false, "petstore.yaml", "paths./pets.get.responses.200.content.application/json.schema")]
        [InlineData(true, "petstore.yaml", "paths./pets.get.responses.default.content.application/json.schema")]
        [InlineData(true, "petstore.yaml", "paths./pets.post.requestBody.content.application/json.schema")]
        [InlineData(true, "petstore.yaml", "paths./pets.post.responses.200.content.application/json.schema")]
        [InlineData(false, "petstore.yaml", "paths./pets/{id}.get.parameters[?(@.name=='id')].schema")]
        [InlineData(false, "petstore.yaml", "paths./pets/{id}.delete.parameters[?(@.name=='id')].schema")]
        [InlineData(true, "petstore.yaml", "components.schemas.Pet")]
        [InlineData(true, "petstore.yaml", "components.schemas.NewPet")]
        [InlineData(true, "petstore.yaml", "components.schemas.Error")]
        [InlineData(true, "no-refs.yaml", "paths./address.post.requestBody.content.application/json.schema")]
        [InlineData(true, "no-refs.yaml", "paths./address.post.requestBody.content.application/json.schema.properties.location")]
        public void DetermineWhenToGenerateSource(bool expectedInline, string documentName, string path)
        {
            var docContents = GetDocumentString(documentName);

            var (document, schema) = GetSchema(docContents, path);
            Assert.NotNull(document);
            Assert.NotNull(schema);

            var target = ConstructTarget(document!, LoadOptions());
            target.EnsureSchemasRegistered(document!, OpenApiContext.From(document!), new());

            var actual = target.ProduceSourceEntry(schema!);

            Assert.Equal(expectedInline, actual);
        }

        private (OpenApiDocument? document, OpenApiSchema? schema) GetSchema(string docContents, string path)
        {
            const string prefix = "components.schemas.";
            var openApiReader = new OpenApiStringReader();
            var document = openApiReader.Read(docContents, out var docDiagnostic);

            if (!path.StartsWith(prefix))
            {
                using var reader = new StringReader(docContents);
                var serializer = new SharpYaml.Serialization.Serializer();
                var documentJObject = Newtonsoft.Json.Linq.JObject.FromObject(serializer.Deserialize(reader));
                var token = documentJObject.SelectToken(path);
                if (token == null)
                {
                    return (document, null);
                }

                var schema = openApiReader.ReadFragment<OpenApiSchema>(token.ToString(), ToSpecVersion((documentJObject["openapi"] ?? documentJObject["swagger"])?.ToObject<string>()), out var openApiDiagnostic);
                if (schema.UnresolvedReference)
                    schema = (OpenApiSchema)document.ResolveReference(schema.Reference);
                return (document, schema);
            }
            else
            {
                return (document, document.Components.Schemas[path.Substring(prefix.Length)]);
            }
        }

        [Theory]
        [InlineData("petstore.yaml", "Pet")]
        [InlineData("petstore.yaml", "NewPet")]
        [InlineData("petstore.yaml", "Error")]
        [InlineData("petstore3.json", "Order")]
        [InlineData("petstore3.json", "Category")]
        [InlineData("petstore3.json", "User")]
        [InlineData("petstore3.json", "Tag")]
        [InlineData("petstore3.json", "Pet")]
        [InlineData("petstore3.json", "ApiResponse")]
        public void TransformModel(string documentName, string model)
        {
            var document = GetDocument(documentName);
            var options = LoadOptions();

            var target = ConstructTarget(document, options);
            OpenApiTransformDiagnostic diagnostic = new();
            target.EnsureSchemasRegistered(document, OpenApiContext.From(document), diagnostic);

            var context = OpenApiContext.From(document).Append(nameof(document.Components), null, document.Components).Append(nameof(document.Components.Schemas), model, document.Components.Schemas[model]);

            var result = target.TransformSchema(document.Components.Schemas[model], diagnostic);

            Snapshot.Match(result?.SourceText, $"Full-{nameof(TransformModel)}.{CSharpNaming.ToTitleCaseIdentifier(documentName, options.ReservedIdentifiers())}.{CSharpNaming.ToTitleCaseIdentifier(model, options.ReservedIdentifiers())}");
        }

        private static CSharpSchemaSourceResolver ConstructTarget(OpenApiDocument document, CSharpSchemaOptions options, string baseNamespace = "PrincipleStudios.Test")
        {
            return new CSharpSchemaSourceResolver(baseNamespace, options, new HandlebarsFactory(HandlebarsTemplateProcess.CreateHandlebars), "");
        }

    }
}
