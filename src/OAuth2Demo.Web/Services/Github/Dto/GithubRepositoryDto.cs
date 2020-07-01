using System.Text.Json.Serialization;

namespace OAuth2Demo.Web.Services.Github.Dto
{
    public class GithubRepositoryDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("html_url")]
        public string Url { get; set; }
    }
}