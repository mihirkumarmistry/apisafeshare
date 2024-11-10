using Microsoft.AspNetCore.Mvc;
using SafeShareAPI.Business;
using SafeShareAPI.Model;

namespace SafeShareAPI.Controllers
{
    [Route("api/[controller]")]
    public class MedicalHistoryController : BaseController
    {
        private readonly MedicalHistoryManager _manager = null;

        public MedicalHistoryController([FromServices] User user)
        {
            _manager = new MedicalHistoryManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(MedicalHistory data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(MedicalHistory data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }

    [Route("api/[controller]")]
    public class AllergieController : BaseController
    {
        private readonly AllergieManager _manager = null;

        public AllergieController([FromServices] User user)
        {
            _manager = new AllergieManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(Allergie data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(Allergie data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }

    [Route("api/[controller]")]
    public class PreviousMedicalConditionController : BaseController
    {
        private readonly PreviousMedicalConditionManager _manager = null;

        public PreviousMedicalConditionController([FromServices] User user)
        {
            _manager = new PreviousMedicalConditionManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(PreviousMedicalCondition data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(PreviousMedicalCondition data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }

    [Route("api/[controller]")]
    public class MedicationController : BaseController
    {
        private readonly MedicationManager _manager = null;

        public MedicationController([FromServices] User user)
        {
            _manager = new MedicationManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(Medication data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(Medication data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }

    [Route("api/[controller]")]
    public class SurgicalHistoryController : BaseController
    {
        private readonly SurgicalHistoryManager _manager = null;

        public SurgicalHistoryController([FromServices] User user)
        {
            _manager = new SurgicalHistoryManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(SurgicalHistory data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(SurgicalHistory data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }

    [Route("api/[controller]")]
    public class DiagnosticTestController : BaseController
    {
        private readonly DiagnosticTestManager _manager = null;

        public DiagnosticTestController([FromServices] User user)
        {
            _manager = new DiagnosticTestManager() { User = user };
        }

        [HttpGet]
        public IActionResult Get()
        {
            return SendResponse(_manager.Select(), _manager.listData);
        }

        [HttpPost]
        public IActionResult Post(DiagnosticTest data)
        {
            return SendResponseMessage(_manager.Save(data));
        }

        [HttpDelete]
        public IActionResult Delete(DiagnosticTest data)
        {
            return SendResponseMessage(_manager.Delete(data));
        }
    }
}
