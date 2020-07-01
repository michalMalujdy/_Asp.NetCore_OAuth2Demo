using AutoMapper;
using OAuth2Demo.Web.ViewModels.Github;

namespace OAuth2Demo.Web.Services.Github.Dto
{
    public class GithubMaps : Profile
    {
        public GithubMaps()
        {
            CreateMap<GithubRepositoryDto, GithubRepositoriesViewModel.RepositoryViewModel>();
        }
    }
}