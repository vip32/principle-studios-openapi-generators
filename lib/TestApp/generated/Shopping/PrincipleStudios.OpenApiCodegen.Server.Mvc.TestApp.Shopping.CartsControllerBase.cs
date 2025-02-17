/*
 * Shopping API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://principle.tools
 */
#nullable enable
#nullable disable warnings
#pragma warning disable

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc.TestApp.Shopping
{ 
    public abstract class CartsControllerBase : global::Microsoft.AspNetCore.Mvc.ControllerBase
    {
        
        /// <summary>
        /// Retrieve a cart identified by the identity
        /// </summary>
        /// <remarks>
        /// Returns a single Cart
        /// </remarks>
        /// <param name="identity">User identity</param>
        [global::Microsoft.AspNetCore.Mvc.HttpGet]
        [global::Microsoft.AspNetCore.Mvc.Route("/api/v1/shopping/carts/{identity}")]
        // Resource request was successful.
        [global::Microsoft.AspNetCore.Mvc.ProducesResponseType(200)] // 
        // Resource request was invalid.
        [global::Microsoft.AspNetCore.Mvc.ProducesResponseType(400, Type = typeof(ValidationProblemDetails))] // application/json
        // Authorization information is missing or invalid.
        [global::Microsoft.AspNetCore.Mvc.ProducesResponseType(401, Type = typeof(ProblemDetails))] // application/json
        // Resource was not found.
        [global::Microsoft.AspNetCore.Mvc.ProducesResponseType(404, Type = typeof(ProblemDetails))] // application/json
        // Unexpected error.
        [global::Microsoft.AspNetCore.Mvc.ProducesResponseType(500, Type = typeof(ProblemDetails))] // application/json
        public async global::System.Threading.Tasks.Task<global::Microsoft.AspNetCore.Mvc.IActionResult> GetByIdentityTypeSafeEntry(
            [global::Microsoft.AspNetCore.Mvc.FromRoute(Name = "identity"), global::System.ComponentModel.DataAnnotations.Required] string identity
        ) => (await GetByIdentity(identity)).ActionResult;

        protected abstract global::System.Threading.Tasks.Task<GetByIdentityActionResult> GetByIdentity(
            string identity);

        public readonly struct GetByIdentityActionResult
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

            private GetByIdentityActionResult(global::Microsoft.AspNetCore.Mvc.IActionResult ActionResult, global::System.Collections.Generic.KeyValuePair<string, string>[] headers)
            {
                this.ActionResult = new HeaderActionResult(ActionResult, headers);
            }

            private GetByIdentityActionResult(int statusCode, params global::System.Collections.Generic.KeyValuePair<string, string>[] headers)
                : this(new global::Microsoft.AspNetCore.Mvc.StatusCodeResult(statusCode), headers)
            {
            }

            private GetByIdentityActionResult(int statusCode, string mediaType, global::System.Type declaredType, object? resultObject, params global::System.Collections.Generic.KeyValuePair<string, string>[] headers)
                : this(new global::Microsoft.AspNetCore.Mvc.ObjectResult(resultObject)
                {
                    ContentTypes = new global::Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection { { new global::Microsoft.Net.Http.Headers.MediaTypeHeaderValue(mediaType) } },
                    DeclaredType = declaredType,
                    StatusCode = statusCode
                }, headers)
            {
            }
            
            /// <summary>
            /// Resource request was successful.
            /// </summary>
            public static GetByIdentityActionResult Ok() =>
                new GetByIdentityActionResult(200);
            
            /// <summary>
            /// Resource request was invalid.
            /// </summary>
            public static GetByIdentityActionResult BadRequest(ValidationProblemDetails result) =>
                new GetByIdentityActionResult(400, "application/json", typeof(ValidationProblemDetails), result);
            
            /// <summary>
            /// Authorization information is missing or invalid.
            /// </summary>
            public static GetByIdentityActionResult Unauthorized(ProblemDetails result) =>
                new GetByIdentityActionResult(401, "application/json", typeof(ProblemDetails), result);
            
            /// <summary>
            /// Resource was not found.
            /// </summary>
            public static GetByIdentityActionResult NotFound(ProblemDetails result) =>
                new GetByIdentityActionResult(404, "application/json", typeof(ProblemDetails), result);
            
            /// <summary>
            /// Unexpected error.
            /// </summary>
            public static GetByIdentityActionResult InternalServerError(ProblemDetails result) =>
                new GetByIdentityActionResult(500, "application/json", typeof(ProblemDetails), result);
            

            /// <summary>Allows for action results not specified in the API</summary>
            public static GetByIdentityActionResult Unsafe(global::Microsoft.AspNetCore.Mvc.IActionResult actionResult, params global::System.Collections.Generic.KeyValuePair<string, string>[] headers) =>
                new GetByIdentityActionResult(actionResult, headers);
        }
    }
}