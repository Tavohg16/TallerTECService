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
    public class BillReportController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public BillReportController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        
        // POST api/manage/billing
        [HttpGet]
        public ActionResult<ActionResponse> CreateBill(BillRequest newBill)
        {
            
            var response = _repository.CreateBill(newBill);
            return Ok(response);

        }

    }
}