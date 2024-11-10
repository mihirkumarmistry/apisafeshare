using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SafeShareAPI.Data;
using SafeShareAPI.Model;
using SafeShareAPI.Model.Common;
using SafeShareAPI.Provider;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using static SafeShareAPI.Provider.AccessProvider;
using static SafeShareAPI.Provider.ConnectionProvider;

namespace SafeShareAPI.Business
{
    public class LoginProcess
    {
        public static ApiResponse LoginMech(AuthModel data, bool checkNewPass = true)
        {
            ApiResponse apiResponse = new() { Status = (byte)Exceptions.Failed };
            try
            {
                Connection connection = GetProvider.GetConnection();
                if (connection != null)
                {
                    var test = EncryptionProvider.Encrypt(data.Password);
                    using DefaultContext defaultContext = new(connection);
                    User user = defaultContext.Users
                                .Include(u => u.UserType)
                                .Where(u => u.Username == data.Username && u.Password == EncryptionProvider.Encrypt(data.Password) && u.IsDeleted == false)
                           .FirstOrDefault();

                    if (user == null) { apiResponse.Message = "Enter valid credentials"; }
                    else if (checkNewPass && user.IsNewPassword) { apiResponse.Message = "Verification panding"; }
                    else if (!user.IsActive) { apiResponse.Message = "User inactivated"; }
                    else
                    {
                        user.Password = "";
                        Claim additionalClaim = new(ClaimTypes.Role, user.UserType.IsAdmin ? Convert.ToString(SystemUserType.Admin) : Convert.ToString(SystemUserType.Staff));
                        DateTime expiry = user.UserType.IsAdmin ? DateTime.Now.Add(TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay) : DateTime.Now.AddMonths(1);
                        user.AccessToken = GetUserAccessToken(user, expiry, additionalClaim);
                        apiResponse = new ApiResponse { Data = user, Status = (byte)Exceptions.Success };
                    }

                }   
                else { apiResponse.Message = "Tenant not found"; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); apiResponse.Status = (byte)Exceptions.Failed; }
            return apiResponse;
        }
    }
    public static class UtilityProvider
    {
        public static bool GetUserFromAccessToken(HttpContext context, ref User user)
        {
            Claim claim = ((ClaimsIdentity)context.User.Identity).Claims.Where(c => c.Type == ClaimTypes.UserData).FirstOrDefault();
            if (claim != null && !string.IsNullOrWhiteSpace(claim.Value)) { user = JsonConvert.DeserializeObject<User>(claim.Value); return true; }
            return false;
        }
        public static bool GetUserFromExpiredAccessToken(HttpContext context, ref User user)
        {
            if (!string.IsNullOrWhiteSpace(Convert.ToString(context.Request.Headers["Authorization"])))
            {
                string authHeader = Convert.ToString(context.Request.Headers["Authorization"]);
                if (!string.IsNullOrWhiteSpace(authHeader))
                {
                    string token = authHeader.Split(" ").LastOrDefault();
                    JwtSecurityTokenHandler tokenHandler = new();
                    Claim claim = ((JwtSecurityToken)tokenHandler.ReadToken(token)).Claims.Where(c => c.Type == ClaimTypes.UserData).FirstOrDefault();
                    if (claim != null && !string.IsNullOrWhiteSpace(claim.Value)) { user = JsonConvert.DeserializeObject<User>(claim.Value); return true; }
                }
            }
            return false;
        }
        public static bool GetDownloadLink(HttpContext context, ref string data)
        {
            if (!string.IsNullOrWhiteSpace(Convert.ToString(context.Request.Headers["Download"])))
            {
                string Header = Convert.ToString(context.Request.Headers["Download"]);
                if (!string.IsNullOrWhiteSpace(Header)) { data = Header; return true; }
            }
            return false;
        }
        public static bool GetUserFromAccessToken(string token, ref User user)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                if (!string.IsNullOrWhiteSpace(token))
                {
                    JwtSecurityTokenHandler tokenHandler = new();
                    Claim claim = ((JwtSecurityToken)tokenHandler.ReadToken(token)).Claims.Where(c => c.Type == ClaimTypes.UserData).FirstOrDefault();
                    if (claim != null && !string.IsNullOrWhiteSpace(claim.Value)) { user = JsonConvert.DeserializeObject<User>(claim.Value); return true; }
                }
            }
            return false;
        }
    }
}