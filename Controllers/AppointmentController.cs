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
    
    [Route("api/manage/appointment")]
    [ApiController]
    [EnableCors("Policy")]
    public class AppointmentController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public AppointmentController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        
        // POST api/manage/appointment
        [HttpPost]
        public ActionResult<ActionResponse> CreateAppointment(Appointment newAppointment)
        {

            var response = _repository.CreateAppointment(newAppointment);
            return Ok(response);

        }

        //GET api/manage/appointment/all
        [HttpGet("all")]
        public ActionResult<MultivalueApp> GetAllAppointments()
        {
            var response = _repository.GetAllAppointments();
            return Ok(response);
        }

    }
}