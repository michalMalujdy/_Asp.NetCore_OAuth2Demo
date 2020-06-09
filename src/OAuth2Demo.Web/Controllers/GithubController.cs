using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2Demo.Web.Services.Github;
using OAuth2Demo.Web.ViewModels.Github;

namespace OAuth2Demo.Web.Controllers
{
    [Route("github")]
    public class GithubController : Controller
    {
        private readonly IGithubService _githubService;
        private readonly IMapper _mapper;

        private const string StateSessionKey = "State";
        private const string GithubAccessTokenSessionKey = "GithubAccessToken";

        public GithubController(IGithubService githubService, IMapper mapper)
        {
            _githubService = githubService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var state = Guid.NewGuid().ToString();

            HttpContext.Session.SetString(StateSessionKey, state);

            var url = _githubService.GetOAuthCodeUrl(state);

            var viewModel = new GithubIndexViewModel(url);

            return View(viewModel);
        }

        [HttpGet("auth-return")]
        public async Task <IActionResult> AuthReturn(
            [FromQuery] string code,
            [FromQuery] string state)
        {
            var stateFromCookie = HttpContext.Session.GetString(StateSessionKey);

            if (stateFromCookie != state)
                return BadRequest("There was a problem with authentication. Please try again.");

            var accessToken = await _githubService.GetAccessToken(code);

            HttpContext.Session.SetString(GithubAccessTokenSessionKey, accessToken);

            return RedirectToAction("Repositories");
        }

        [HttpGet("repositories")]
        public async Task<IActionResult> Repositories()
        {
            var accessToken = HttpContext.Session.GetString(GithubAccessTokenSessionKey);

            if (string.IsNullOrEmpty(accessToken))
                return BadRequest("There was a problem with authentication. Please try again.");

            var repositoriesDto = await _githubService.GetRepositories(accessToken);

            var repositoriesViewModel = _mapper.Map<List<GithubRepositoriesViewModel.RepositoryViewModel>>(repositoriesDto);

            return View(new GithubRepositoriesViewModel{ Repositories = repositoriesViewModel});
        }
    }
}