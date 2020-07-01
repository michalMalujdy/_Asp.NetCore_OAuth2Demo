using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuth2Demo.Web.Services.Github;
using OAuth2Demo.Web.Services.Google;
using OAuth2Demo.Web.Settings;
using OAuth2Demo.Web.Settings.Github;
using OAuth2Demo.Web.Settings.Google;

namespace OAuth2Demo.Web.ConfigurationExtensions
{
    public static class ConfigureCustomServicesExtension
    {
        public static void AddConfiguredCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.BindSettingsSingleton<GithubSessionSettings>(configuration, "GithubSession");
            services.BindSettingsSingleton<GithubServiceSettings>(configuration, "GithubService");
            services.AddTransient<IGithubService, GithubService>();

            services.BindSettingsSingleton<GoogleSessionSettings>(configuration, "GoogleSession");
            services.BindSettingsSingleton<GoogleServiceSettings>(configuration, "GoogleService");
            services.AddTransient<IGoogleService, GoogleService>();
        }

        private static void BindSettingsSingleton<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionName)
            where T : class
        {
            var settings = Activator.CreateInstance<T>();
            configuration.Bind(sectionName, settings);
            services.AddSingleton(settings);
        }
    }
}