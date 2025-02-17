/*
 * AllOf Example
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://principle.tools
 */
#pragma warning disable
#nullable enable
#nullable disable warnings

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc.TestApp.AllOf
{
    public partial record ContactWithId(
        [property: global::System.Text.Json.Serialization.JsonPropertyName("firstName")][param: global::System.ComponentModel.DataAnnotations.Required] string FirstName, 
        [property: global::System.Text.Json.Serialization.JsonPropertyName("lastName")][param: global::System.ComponentModel.DataAnnotations.Required] string LastName, 
        [property: global::System.Text.Json.Serialization.JsonPropertyName("id")][param: global::System.ComponentModel.DataAnnotations.Required] string Id
    );
}
