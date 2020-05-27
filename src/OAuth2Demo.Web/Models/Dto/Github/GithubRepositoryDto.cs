using System.Text.Json.Serialization;

namespace OAuth2Demo.Web.Models.Dto.Github
{
    public class GithubRepositoryDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("html_url")]
        public string Url { get; set; }
    }
}