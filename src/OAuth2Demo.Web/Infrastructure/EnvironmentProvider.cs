using System;

namespace OAuth2Demo.Web.Infrastructure
{
    public static class EnvironmentProvider
    {
        public static readonly string Current = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? nameof (Production);
        public const string Production = "Production";

        public static bool IsProduction()
        {
            return EnvironmentProvider.Current == "Production";
        }
    }
}