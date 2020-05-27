namespace OAuth2Demo.Web.Models.ViewModels.Github
{
    public class GithubIndexViewModel
    {
        public string GithubOAuthUrl { get; set; }

        public GithubIndexViewModel(string githubOAuthUrl)
        {
            GithubOAuthUrl = githubOAuthUrl;
        }
    }
}