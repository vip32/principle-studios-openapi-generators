﻿using HandlebarsDotNet;
using PrincipleStudios.OpenApi.TypeScript;
using System.IO;
using System.Linq;

namespace PrincipleStudios.OpenApiCodegen.Client.TypeScript
{
    public static class OperationHandlebarsTemplateProcess
    {
        public static IHandlebars CreateHandlebars()
        {
            var result = HandlebarsTemplateProcess.CreateHandlebars();

            foreach (var resourceName in typeof(OperationHandlebarsTemplateProcess).Assembly.GetManifestResourceNames().Where(n => n.EndsWith(".handlebars")))
                result.AddTemplate(typeof(OperationHandlebarsTemplateProcess).Assembly, resourceName);

            return result;
        }

        public static string ProcessOperation(this IHandlebars handlebars, templates.OperationTemplate operationTemplate)
        {
            var template = handlebars.Configuration.RegisteredTemplates["operation"];

            using var sr = new StringWriter();
            var dict = HandlebarsTemplateProcess.ToDictionary<templates.OperationTemplate>(operationTemplate);
            template(sr, dict);
            return sr.ToString();
        }

        public static string ProcessBarrelFile(this IHandlebars handlebars, templates.OperationBarrelFileModel barrelFileModel)
        {
            var template = handlebars.Configuration.RegisteredTemplates["operationBarrelFile"];

            using var sr = new StringWriter();
            var dict = HandlebarsTemplateProcess.ToDictionary<templates.OperationBarrelFileModel>(barrelFileModel);
            template(sr, dict);
            return sr.ToString();
        }

    }
}
