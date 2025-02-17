﻿using Microsoft.OpenApi.Models;
using PrincipleStudios.OpenApi.CSharp.templates;
using PrincipleStudios.OpenApi.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrincipleStudios.OpenApi.CSharp;
using Microsoft.OpenApi.Any;

namespace PrincipleStudios.OpenApi.CSharp
{
    public class CSharpClientTransformer : ISourceProvider
    {
        private readonly ISchemaSourceResolver<InlineDataType> csharpSchemaResolver;
        private readonly OpenApiDocument document;
        private readonly string baseNamespace;
        private readonly CSharpSchemaOptions options;
        private readonly string versionInfo;
        private readonly HandlebarsFactory handlebarsFactory;

        public CSharpClientTransformer(ISchemaSourceResolver<InlineDataType> csharpSchemaResolver, OpenApiDocument document, string baseNamespace, CSharpSchemaOptions options, string versionInfo, HandlebarsFactory handlebarsFactory)
        {
            this.csharpSchemaResolver = csharpSchemaResolver;
            this.document = document;
            this.baseNamespace = baseNamespace;
            this.options = options;
            this.versionInfo = versionInfo;
            this.handlebarsFactory = handlebarsFactory;
        }

        public SourceEntry TransformOperations(OpenApiTransformDiagnostic diagnostic)
        {
            csharpSchemaResolver.EnsureSchemasRegistered(document, OpenApiContext.From(document), diagnostic);

            var className = CSharpNaming.ToClassName("operations", options.ReservedIdentifiers());

            var resultOperations = new List<Operation>();
            var visitor = new OperationVisitor(csharpSchemaResolver, options, controllerClassName: className);
            visitor.Visit(document, OpenApiContext.From(document), new OperationVisitor.Argument(diagnostic, resultOperations.Add));

            resultOperations = (from operation in resultOperations
                                select operation with { path = operation.path.Substring(1) }).ToList();

            var template = new templates.FullTemplate(
                header: new templates.PartialHeader(
                    appName: document.Info.Title,
                    appDescription: document.Info.Description,
                    version: document.Info.Version,
                    infoEmail: document.Info.Contact?.Email,
                    codeGeneratorVersionInfo: versionInfo
                ),

                packageName: baseNamespace,
                className: className,

                operations: resultOperations.ToArray()
            );

            var entry = handlebarsFactory.Handlebars.ProcessController(template);
            return new SourceEntry
            {
                Key = $"{baseNamespace}.{className}.cs",
                SourceText = entry,
            };
        }

        public string SanitizeGroupName(string groupName)
        {
            return CSharpNaming.ToClassName(groupName + " client", options.ReservedIdentifiers());
        }

        internal SourceEntry TransformAddServicesHelper(OpenApiTransformDiagnostic diagnostic)
        {
            return new SourceEntry
            {
                Key = $"{baseNamespace}.AddServicesExtensions.cs",
                SourceText = handlebarsFactory.Handlebars.ProcessAddServices(new templates.AddServicesModel(
                    header: new templates.PartialHeader(
                        appName: document.Info.Title,
                        appDescription: document.Info.Description,
                        version: document.Info.Version,
                        infoEmail: document.Info.Contact?.Email,
                        codeGeneratorVersionInfo: versionInfo
                    ),
                    methodName: CSharpNaming.ToMethodName(document.Info.Title, options.ReservedIdentifiers()),
                    packageName: baseNamespace
                )),
            };
        }

        public IEnumerable<SourceEntry> GetSources(OpenApiTransformDiagnostic diagnostic)
        {
            yield return TransformOperations(diagnostic);
            //yield return TransformAddServicesHelper(diagnostic);

            foreach (var source in csharpSchemaResolver.GetSources(diagnostic))
                yield return source;
        }
    }
}
