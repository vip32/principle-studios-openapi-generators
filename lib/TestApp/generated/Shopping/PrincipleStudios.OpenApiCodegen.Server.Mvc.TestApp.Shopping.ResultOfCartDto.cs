/*
 * Shopping API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://principle.tools
 */
#pragma warning disable
#nullable enable
#nullable disable warnings

namespace PrincipleStudios.OpenApiCodegen.Server.Mvc.TestApp.Shopping
{
    public partial record ResultOfCartDto(
        [property: global::System.Text.Json.Serialization.JsonPropertyName("messages")][property: global::System.Text.Json.Serialization.JsonIgnore(Condition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)] global::PrincipleStudios.OpenApiCodegen.Json.Extensions.Optional<global::System.Collections.Generic.IEnumerable<string>?>? Messages, 
        [property: global::System.Text.Json.Serialization.JsonPropertyName("succeeded")][property: global::System.Text.Json.Serialization.JsonIgnore(Condition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)] global::PrincipleStudios.OpenApiCodegen.Json.Extensions.Optional<bool>? Succeeded, 
        [property: global::System.Text.Json.Serialization.JsonPropertyName("data")][property: global::System.Text.Json.Serialization.JsonIgnore(Condition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)] global::PrincipleStudios.OpenApiCodegen.Json.Extensions.Optional<CartDto>? Data
    );
}
