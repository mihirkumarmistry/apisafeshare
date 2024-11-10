using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Model;

namespace SafeShareAPI.Controllers
{
    [Route("api/[controller]")]
    public class PatientController : BaseController
    {
        private readonly PatientManager _manager = null;
        
        public PatientController([FromServices] User user)
        {
            _manager = new PatientManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(Patient data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(Patient data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }

    }
}
