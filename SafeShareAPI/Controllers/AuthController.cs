using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Model;

namespace SafeShareAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        [HttpPost]
        public IActionResult Post(AuthModel data)
        {
            return SendResponse(LoginProcess.LoginMech(data));
        }
    }
}
