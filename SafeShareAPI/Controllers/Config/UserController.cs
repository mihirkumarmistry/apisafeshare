using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Extensions;
using SafeShareAPI.Model;
using static SafeShareAPI.Provider.AccessProvider;

namespace SafeShareAPI.Controllers
{
    [AuthorizeRoles(SystemUserType.Admin), Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly UserManager _manager = null;

        public UsersController([FromServices] User user)
        {
            _manager = new UserManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(User data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(User data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }
}
