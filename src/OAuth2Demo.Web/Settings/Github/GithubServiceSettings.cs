namespace OAuth2Demo.Web.Settings.Github
{
    public class GithubServiceSettings
    {
        public string GetOAuth2CodeUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string GetTokenUrl { get; set; }

        public string GetReposUrl { get; set; }
    }
}