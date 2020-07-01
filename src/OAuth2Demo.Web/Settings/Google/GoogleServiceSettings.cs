namespace OAuth2Demo.Web.Settings.Google
{
    public class GoogleServiceSettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string GetOAuth2CodeUrl { get; set; }

        public string GetTokensUrl { get; set; }

        public string GetUserInfoUrl { get; set; }
    }
}