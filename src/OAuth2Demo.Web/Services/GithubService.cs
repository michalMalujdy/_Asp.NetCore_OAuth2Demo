using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OAuth2Demo.Web.Models.Dto.Github;
using OAuth2Demo.Web.Settings;

namespace OAuth2Demo.Web.Services
{
    public class GithubService : IGithubService
    {
        private readonly GithubSettings _settings;
        private static HttpClient _httpClient;

        public GithubService(GithubSettings settings)
        {
            _settings = settings;
            _httpClient = new HttpClient();
        }

        public string GetOAuthCodeUrl(string state)
            => _settings.GetOAuth2CodeUrl +
               "?response_type=code" +
               "&scope=user public_repo" +
               $"&client_id={_settings.ClientId}" +
               $"&state={state}";

        public async Task<string> GetAccessToken(string code)
        {
            var getAccessTokenUrl = _settings.GetTokenUrl +
                                    $"?client_id={_settings.ClientId}" +
                                    $"&client_secret={_settings.ClientSecret}" +
                                    $"&code={code}";

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await _httpClient.GetAsync(getAccessTokenUrl);

            var responseContent = await response.Content.ReadAsStringAsync();
            var accessTokenDto = JsonSerializer.Deserialize<GetAccessTokenResponseDto>(responseContent);

            return accessTokenDto.AccessToken;
        }

        public async Task<List<GithubRepositoryDto>> GetRepositories(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "OAuthDemo");

            var response = await _httpClient.GetAsync("https://api.github.com/user/repos?sort=created&direction=desc");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Problem while getting repositories");

            var responseContent = await response.Content.ReadAsStringAsync();
            var repositoriesDto = JsonSerializer.Deserialize<List<GithubRepositoryDto>>(responseContent);

            return repositoriesDto;
        }
    }
}