using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace OAuth2Demo.Web.Infrastructure
{

    public static class ConfigurationBuilderFactory
    {
        public static IConfigurationBuilder Create()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings." + Environment.MachineName + ".json", true, true)
                .AddEnvironmentVariables();
        }
    }
}