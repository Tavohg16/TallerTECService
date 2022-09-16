using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{
    //AppointmentController hereda la clase ControllerBase, utilizada para el manejo
    //del endpoints.
    //ApiController identifica a la clase como un controlador en el framework.
    //TallerController se encarga de manejar el endpoint que permite a los usuarios hacer login.
    //Route especifica la ruta para este controlador. En este caso local:
    //http://localhost:5075/api/manage/appointment
    
    [Route("api/manage/billing")]
    [ApiController]
    [EnableCors("Policy")]
    public class BillController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public BillController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        
        // POST api/manage/billing
        [HttpPost]
        public ActionResult<ActionResponse> CreateBill(BillRequest newBill)
        {
            
            var response = _repository.CreateBill(newBill);
            return Ok(response);

        }

    }


    [Route("api/manage/reporting")]
    [ApiController]
    [EnableCors("Policy")]
    public class ReportController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public ReportController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        
        // POST api/manage/reporting
        [HttpPost]
        public ActionResult<ActionResponse> CreateBill(ReportRequest newReport)
        {
            
            var response = _repository.CreateReport(newReport);
            return Ok(response);

        }

    }
}