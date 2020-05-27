using System;
using Microsoft.Extensions.DependencyInjection;

namespace OAuth2Demo.Web.ConfigurationExtensions
{
    public static class ConfigureSessionExtension
    {
        public static void AddConfiguredSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = "OAuthDemo.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = false;
            });
        }
    }
}