/*
 * Swagger Petstore
 *
 * A sample API that uses a petstore as an example to demonstrate features in the OpenAPI 3.0 specification
 *
 * OpenAPI spec version: 1.0.0
 * Contact: apiteam@swagger.io
 * Generated by: https://principle.tools
 */
#pragma warning disable
#nullable enable
#nullable disable warnings

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc.TestApp.PetStore
{
    public partial record Pet(
        [property: global::System.Text.Json.Serialization.JsonPropertyName("name")][param: global::System.ComponentModel.DataAnnotations.Required] string Name, 
        [property: global::System.Text.Json.Serialization.JsonPropertyName("tag")][property: global::System.Text.Json.Serialization.JsonIgnore(Condition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)] global::PrincipleStudios.OpenApiCodegen.Json.Extensions.Optional<string>? Tag, 
        [property: global::System.Text.Json.Serialization.JsonPropertyName("id")][param: global::System.ComponentModel.DataAnnotations.Required] long Id
    );
}
