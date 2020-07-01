using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2Demo.Web.Services.Github;
using OAuth2Demo.Web.Settings.Github;
using OAuth2Demo.Web.ViewModels.Github;

namespace OAuth2Demo.Web.Controllers
{
    [Route("github")]
    public class GithubController : Controller
    {
        private readonly IGithubService _githubService;
        private readonly GithubSessionSettings _githubSessionSettings;
        private readonly IMapper _mapper;

        public GithubController(
            IGithubService githubService,
            GithubSessionSettings githubSessionSettings,
            IMapper mapper)
        {
            _githubService = githubService;
            _githubSessionSettings = githubSessionSettings;
            _mapper = mapper;
        }

        [HttpGet("auth-return")]
        public async Task <IActionResult> AuthReturn(
            [FromQuery] string code,
            [FromQuery] string state)
        {
            var stateFromCookie = HttpContext.Session.GetString(_githubSessionSettings.StateKey);

            if (stateFromCookie != state)
                return RedirectToAction("Index", "Home");

            var accessToken = await _githubService.GetAccessToken(code);

            HttpContext.Session.SetString(_githubSessionSettings.AccessTokenKey, accessToken);

            return RedirectToAction("Repositories");
        }

        [HttpGet("repositories")]
        public async Task<IActionResult> Repositories()
        {
            var accessToken = HttpContext.Session.GetString(_githubSessionSettings.AccessTokenKey);

            if (string.IsNullOrEmpty(accessToken))
                return RedirectToAction("Index", "Home");

            var repositoriesDto = await _githubService.GetRepositories(accessToken);

            var repositoriesViewModel = _mapper.Map<List<GithubRepositoriesViewModel.RepositoryViewModel>>(repositoriesDto);

            return View(new GithubRepositoriesViewModel{ Repositories = repositoriesViewModel });
        }
    }
}