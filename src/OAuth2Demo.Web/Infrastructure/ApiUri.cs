using Microsoft.AspNetCore.Http;

namespace OAuth2Demo.Web.Infrastructure
{
    public static class ApiUri
    {
        public static string GetGoogleRedirectUri(HttpContext context)
            //=> $"{context.Request.Scheme}://{context.Request.Host}/google/auth-return";
            => "https://oauth-m-malujdy.azurewebsites.net/google/auth-return";
    }
}