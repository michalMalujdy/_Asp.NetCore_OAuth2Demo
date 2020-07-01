using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2Demo.Web.Infrastructure;
using OAuth2Demo.Web.Services.Github;
using OAuth2Demo.Web.Services.Google;
using OAuth2Demo.Web.Settings.Github;
using OAuth2Demo.Web.Settings.Google;
using OAuth2Demo.Web.ViewModels.Home;

namespace OAuth2Demo.Web.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IGoogleService _googleService;
        private readonly GoogleSessionSettings _googleSessionSettings;

        private readonly IGithubService _githubService;
        private readonly GithubSessionSettings _githubSessionSettings;

        public HomeController(
            IGoogleService googleService,
            GoogleSessionSettings googleSessionSettings,
            IGithubService githubService,
            GithubSessionSettings githubSessionSettings)
        {
            _googleService = googleService;
            _googleSessionSettings = googleSessionSettings;

            _githubService = githubService;
            _githubSessionSettings = githubSessionSettings;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            var googleState = SetNewSessionState(_googleSessionSettings.StateKey);
            var redirectUri = ApiUri.GetGoogleRedirectUri(HttpContext);
            var googleLoginUrl = _googleService.GetLoginUrl(googleState, redirectUri);

            var githubState = SetNewSessionState(_githubSessionSettings.StateKey);
            var githubOAuthUrl = _githubService.GetOAuthCodeUrl(githubState);

            var viewModel = new IndexViewModel
            {
                GoogleLoginUrl = googleLoginUrl,
                GithubOAuthUrl = githubOAuthUrl
            };

            return View(viewModel);
        }

        private string SetNewSessionState(string key)
        {
            var state = Guid.NewGuid().ToString();
            HttpContext.Session.SetString(key, state);

            return state;
        }
    }
}