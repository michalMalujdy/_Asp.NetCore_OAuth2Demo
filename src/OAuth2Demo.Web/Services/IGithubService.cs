using System.Collections.Generic;
using System.Threading.Tasks;
using OAuth2Demo.Web.Models.Dto.Github;

namespace OAuth2Demo.Web.Services
{
    public interface IGithubService
    {
        string GetOAuthCodeUrl(string state);

        Task<string> GetAccessToken(string code);

        Task<List<GithubRepositoryDto>> GetRepositories(string accessToken);
    }
}