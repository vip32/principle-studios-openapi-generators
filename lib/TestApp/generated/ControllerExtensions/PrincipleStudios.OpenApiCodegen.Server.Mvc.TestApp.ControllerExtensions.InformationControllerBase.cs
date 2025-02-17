/*
 * OAuth Scopes Sample
 *
 * A sample API that uses oauth scopes
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://principle.tools
 */
#nullable enable
#nullable disable warnings
#pragma warning disable

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc.TestApp.ControllerExtensions
{ 
    public abstract class InformationControllerBase : global::Microsoft.AspNetCore.Mvc.ControllerBase
    {
        
        /// <param name="data">base-64 encoded data</param>
        [global::Microsoft.AspNetCore.Mvc.HttpGet]
        [global::Microsoft.AspNetCore.Mvc.Route("/api/info")]
        // Sample Response
        [global::Microsoft.AspNetCore.Mvc.ProducesResponseType(200, Type = typeof(string))] // application/json
        public async global::System.Threading.Tasks.Task<global::Microsoft.AspNetCore.Mvc.IActionResult> GetInfoTypeSafeEntry(
            [global::Microsoft.AspNetCore.Mvc.FromQuery(Name = "data")] byte[]? data
        ) => (await GetInfo(data)).ActionResult;

        protected abstract global::System.Threading.Tasks.Task<GetInfoActionResult> GetInfo(
            byte[]? data);

        public readonly struct GetInfoActionResult
        {
            public readonly global::Microsoft.AspNetCore.Mvc.IActionResult ActionResult;

            private class HeaderActionResult : global::Microsoft.AspNetCore.Mvc.IActionResult
            {
                private readonly global::Microsoft.AspNetCore.Mvc.IActionResult original;
                private readonly global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, string>> headers;

                public HeaderActionResult(global::Microsoft.AspNetCore.Mvc.IActionResult original, global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, string>> headers)
                {
                    this.original = original;
                    this.headers = headers;
                }

                public global::System.Threading.Tasks.Task ExecuteResultAsync(global::Microsoft.AspNetCore.Mvc.ActionContext context)
                {
                    foreach (var header in headers)
                        context.HttpContext.Response.Headers[header.Key] = global::Microsoft.Extensions.Primitives.StringValues.Concat(context.HttpContext.Response.Headers[header.Key], header.Value);
                    return original.ExecuteResultAsync(context);
                }
            }

            private GetInfoActionResult(global::Microsoft.AspNetCore.Mvc.IActionResult ActionResult, global::System.Collections.Generic.KeyValuePair<string, string>[] headers)
            {
                this.ActionResult = new HeaderActionResult(ActionResult, headers);
            }

            private GetInfoActionResult(int statusCode, params global::System.Collections.Generic.KeyValuePair<string, string>[] headers)
                : this(new global::Microsoft.AspNetCore.Mvc.StatusCodeResult(statusCode), headers)
            {
            }

            private GetInfoActionResult(int statusCode, string mediaType, global::System.Type declaredType, object? resultObject, params global::System.Collections.Generic.KeyValuePair<string, string>[] headers)
                : this(new global::Microsoft.AspNetCore.Mvc.ObjectResult(resultObject)
                {
                    ContentTypes = new global::Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection { { new global::Microsoft.Net.Http.Headers.MediaTypeHeaderValue(mediaType) } },
                    DeclaredType = declaredType,
                    StatusCode = statusCode
                }, headers)
            {
            }
            
            /// <summary>
            /// Sample Response
            /// </summary>
            public static GetInfoActionResult Ok(string result) =>
                new GetInfoActionResult(200, "application/json", typeof(string), result);
            

            /// <summary>Allows for action results not specified in the API</summary>
            public static GetInfoActionResult Unsafe(global::Microsoft.AspNetCore.Mvc.IActionResult actionResult, params global::System.Collections.Generic.KeyValuePair<string, string>[] headers) =>
                new GetInfoActionResult(actionResult, headers);
        }
    }
}