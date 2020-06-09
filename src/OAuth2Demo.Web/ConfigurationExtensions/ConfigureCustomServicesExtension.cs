using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuth2Demo.Web.Services.Github;
using OAuth2Demo.Web.Services.Google;
using OAuth2Demo.Web.Settings;

namespace OAuth2Demo.Web.ConfigurationExtensions
{
    public static class ConfigureCustomServicesExtension
    {
        public static void AddConfiguredCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.BindSettingsSingleton<GithubSettings>(configuration, "Github");
            services.AddTransient<IGithubService, GithubService>();

            services.BindSettingsSingleton<GoogleSettings>(configuration, "Google");
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