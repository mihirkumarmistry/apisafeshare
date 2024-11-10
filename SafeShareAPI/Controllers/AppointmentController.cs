using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Model;

namespace SafeShareAPI.Controllers
{
    [Route("api/[controller]")]
    public class AppointmentController : BaseController
    {
        private readonly AppointmentManager _manager = null;

        public AppointmentController([FromServices] User user)
        {
            _manager = new AppointmentManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(Appointment data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(Appointment data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }
}

