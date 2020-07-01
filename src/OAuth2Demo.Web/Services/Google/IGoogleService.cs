using System.Threading.Tasks;
using OAuth2Demo.Web.Services.Google.Dto;

namespace OAuth2Demo.Web.Services.Google
{
    public interface IGoogleService
    {
        string GetLoginUrl(string state, string redirectUri);

        Task<GetGoogleTokensResponseDto> GetTokensFromApi(string code, string redirectUri);

        Task<GetGoogleUserInfoResponseDto> GetUserInfoFromApi(string accessToken);
    }
}