using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Model;

namespace SafeShareAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : BaseController
    {
        private readonly CommonManager _manager = null;

        public CommonController([FromServices] User user)
        {
            _manager = new CommonManager() { User = user };
        }

        [HttpGet("DoctorList")]
        public IActionResult GetDoctor()
        {
            return SendResponse(_manager.DoctorList(), _manager.listData);
        }

        [HttpGet("PatientList")]
        public IActionResult GetPatient()
        {
            return SendResponse(_manager.PatientList(), _manager.listData);
        }

        [HttpGet("AppointmentList")]
        public IActionResult GetAppointment()
        {
            return SendResponse(_manager.AppointmentList(), _manager.listData);
        }
    }

}
