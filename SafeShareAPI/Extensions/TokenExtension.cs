using Newtonsoft.Json;
using SafeShareAPI.Model;
using System.Security.Claims;

namespace SafeShareAPI.Extensions
{
    public static class TokenExtension
    {
        public static User GetUserFromAccessToken(HttpContext context)
        {
            Claim claim = ((ClaimsIdentity)context.User.Identity).Claims.Where(c => c.Type == ClaimTypes.UserData).FirstOrDefault();
            return claim != null && !string.IsNullOrWhiteSpace(claim.Value) ? JsonConvert.DeserializeObject<User>(claim.Value) : null;
        }
        public static void AddExtTokenDependency(this IServiceCollection services)
        {
            services.AddScoped(provider => { return GetUserFromAccessToken(provider.GetService<IHttpContextAccessor>().HttpContext); });
        }
    }
}
