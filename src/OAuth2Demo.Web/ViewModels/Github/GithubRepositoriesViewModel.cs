using System.Collections.Generic;

namespace OAuth2Demo.Web.ViewModels.Github
{
    public class GithubRepositoriesViewModel
    {
        public List<RepositoryViewModel> Repositories { get; set; }

        public class RepositoryViewModel
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }
}