using AutoMapper;
using OAuth2Demo.Web.Models.Dto.Github;
using OAuth2Demo.Web.Models.ViewModels.Github;

namespace OAuth2Demo.Web.Models
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<GithubRepositoryDto, GithubRepositoriesViewModel.RepositoryViewModel>();
        }
    }
}