namespace OAuth2Demo.Web.ViewModels.Google
{
    public class GoogleIndexViewModel
    {
        public string GoogleOAuthUrl { get; private set; }

        public GoogleIndexViewModel(string googleOAuthUrl)
        {
            GoogleOAuthUrl = googleOAuthUrl;
        }
    }
}