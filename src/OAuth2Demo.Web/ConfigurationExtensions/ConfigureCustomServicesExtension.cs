using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuth2Demo.Web.Services;
using OAuth2Demo.Web.Settings;

namespace OAuth2Demo.Web.ConfigurationExtensions
{
    public static class ConfigureCustomServicesExtension
    {
        public static void AddConfiguredCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var githubSettings = new GithubSettings();
            configuration.Bind("Github", githubSettings);
            services.AddSingleton(githubSettings);

            services.AddTransient<IGithubService, GithubService>();
        }
    }
}