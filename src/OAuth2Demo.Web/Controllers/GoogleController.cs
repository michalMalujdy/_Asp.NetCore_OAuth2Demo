using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2Demo.Web.Services.Google;
using OAuth2Demo.Web.ViewModels.Google;

namespace OAuth2Demo.Web.Controllers
{
    [Route("google")]
    public class GoogleController : Controller
    {
        private readonly IGoogleService _googleService;

        private const string StateSessionKey = "State";
        private const string GoogleAccessTokenSessionKey = "GoogleAccessToken";
        private const string GoogleIdTokenSessionKey = "GoogleIdToken";

        public GoogleController(IGoogleService googleService)
        {
            _googleService = googleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var state = Guid.NewGuid().ToString();

            HttpContext.Session.SetString(StateSessionKey, state);

            var url = _googleService.GetLoginUrl(state, GetRedirectUri());

            return View(new GoogleIndexViewModel(url));
        }

        [HttpGet("auth-return")]
        public async Task<IActionResult> AuthReturn(
            [FromQuery] string code,
            [FromQuery] string state)
        {
            var stateFromCookie = HttpContext.Session.GetString(StateSessionKey);

            if (stateFromCookie != state)
                return BadRequest("There was a problem with authentication. Please try again.");

            var tokens = await _googleService.GetTokensFromApi(code, GetRedirectUri());

            HttpContext.Session.SetString(GoogleAccessTokenSessionKey, tokens.AccessToken);
            HttpContext.Session.SetString(GoogleIdTokenSessionKey, tokens.IdToken);

            return RedirectToAction("Profile");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var accessToken = HttpContext.Session.GetString(GoogleAccessTokenSessionKey);

            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("There was a problem with authentication. Please try again.");

            var idToken = HttpContext.Session.GetString(GoogleIdTokenSessionKey);

            if (string.IsNullOrEmpty(idToken))
                return BadRequest("There was a problem with authentication. Please try again.");

            var userInfo = await _googleService.GetUserInfoFromApi(accessToken);

            var viewModel = new GoogleProfileViewModel
            {
                GoogleId = userInfo.GoogleId,
                Email = userInfo.Email,
                IsEmailVerified = userInfo.IsEmailVerified,
                PictureUrl = userInfo.PictureUrl
            };

            return View(viewModel);
        }

        // private string GetRedirectUri()
        //     => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/google/auth-return";

        private string GetRedirectUri()
            => "https://oauth-m-malujdy.azurewebsites.net/google/auth-return";
    }
}