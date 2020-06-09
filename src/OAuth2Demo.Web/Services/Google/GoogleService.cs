using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OAuth2Demo.Web.Services.Google.Dto;
using OAuth2Demo.Web.Settings;

namespace OAuth2Demo.Web.Services.Google
{
    public class GoogleService : IGoogleService
    {
        private readonly GoogleSettings _settings;
        private static HttpClient _getTokensClient;
        private static HttpClient _getUserInfoClient;

        public GoogleService(GoogleSettings settings)
        {
            _settings = settings;
            _getTokensClient = new HttpClient();
            _getUserInfoClient = new HttpClient();
        }

        public string GetLoginUrl(string state, string redirectUri)
            => _settings.GetOAuth2CodeUrl +
               "?response_type=code" +
               "&scope=openid email" +
               $"&client_id={_settings.ClientId}" +
               $"&state={state}" +
               $"&redirect_uri={redirectUri}";

        public async Task<GetGoogleTokensResponseDto> GetTokensFromApi(string code, string redirectUri)
        {
            var requestBody = new GetGoogleTokensRequestDto
            {
                GrantType = "authorization_code",
                RedirectUri = redirectUri,
                Code = code,
                ClientId = _settings.ClientId,
                ClientSecret = _settings.ClientSecret
            };

            var requestBodyJson = JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            var response = await _getTokensClient.PostAsync(_settings.GetTokensUrl, requestContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Problem while obtaining tokens");

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetGoogleTokensResponseDto>(responseContent);
        }

        public async Task<GetGoogleUserInfoResponseDto> GetUserInfoFromApi(string accessToken)
        {
            _getUserInfoClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await _getUserInfoClient.GetAsync(_settings.GetUserInfoUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Problem while obtaining user info");

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetGoogleUserInfoResponseDto>(responseContent);
        }
    }
}