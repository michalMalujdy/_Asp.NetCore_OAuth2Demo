using System.Text.Json.Serialization;

namespace OAuth2Demo.Web.Services.Google.Dto
{
    public class GetGoogleUserInfoResponseDto
    {
        [JsonPropertyName("sub")]
        public string GoogleId { get; set; }

        [JsonPropertyName("picture")]
        public string PictureUrl { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("email_verified")]
        public bool IsEmailVerified { get; set; }
    }
}