using System.Text.Json.Serialization;

namespace Lavenue.AuthenticationService.Model.Dto;

public class UserDto
{
    [JsonPropertyName("client_id")] public string ClientId { get; set; }

    [JsonPropertyName("email")] public string Email { get; set; }

    [JsonPropertyName("password")] public string Password { get; set; }

    [JsonPropertyName("connection")] public string Connection { get; set; }

    [JsonPropertyName("username")] public string UserName { get; set; }

    [JsonPropertyName("given-name")] public string GivenName { get; set; }

    [JsonPropertyName("family_name")] public string FamilyName { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("nickname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string NickName { get; set; }

    [JsonPropertyName("picture")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Picture { get; set; }
}