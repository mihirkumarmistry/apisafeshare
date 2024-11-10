using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Model;

namespace SafeShareAPI.Controllers
{
    [Route("api/[controller]")]
    public class BillController : BaseController
    {
        private readonly BillManager _manager = null;

        public BillController([FromServices] User user)
        {
            _manager = new BillManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(Bill data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(Bill data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }
}
