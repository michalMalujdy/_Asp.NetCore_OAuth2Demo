using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2Demo.Web.Infrastructure;
using OAuth2Demo.Web.Services.Google;
using OAuth2Demo.Web.Settings.Google;
using OAuth2Demo.Web.ViewModels.Google;

namespace OAuth2Demo.Web.Controllers
{
    [Route("google")]
    public class GoogleController : Controller
    {
        private readonly IGoogleService _googleService;
        private readonly GoogleSessionSettings _googleSessionSettings;

        public GoogleController(IGoogleService googleService, GoogleSessionSettings googleSessionSettings)
        {
            _googleService = googleService;
            _googleSessionSettings = googleSessionSettings;
        }

        [HttpGet("auth-return")]
        public async Task<IActionResult> AuthReturn(
            [FromQuery] string code,
            [FromQuery] string state)
        {
            var stateFromCookie = HttpContext.Session.GetString(_googleSessionSettings.StateKey);

            if (stateFromCookie != state)
                return RedirectToAction("Index", "Home");

            var redirectUri = ApiUri.GetGoogleRedirectUri(HttpContext);
            var tokens = await _googleService.GetTokensFromApi(code, redirectUri);

            HttpContext.Session.SetString(_googleSessionSettings.AccessTokenKey, tokens.AccessToken);
            HttpContext.Session.SetString(_googleSessionSettings.IdTokenKey, tokens.IdToken);

            return RedirectToAction("Profile");
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var accessToken = HttpContext.Session.GetString(_googleSessionSettings.AccessTokenKey);

            if (string.IsNullOrEmpty(accessToken))
                return RedirectToAction("Index", "Home");

            var idToken = HttpContext.Session.GetString(_googleSessionSettings.IdTokenKey);

            if (string.IsNullOrEmpty(idToken))
                return RedirectToAction("Index", "Home");

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
    }
}