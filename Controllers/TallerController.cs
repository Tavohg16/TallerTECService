using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{

    //TallerController hereda la clase ControllerBase, utilizada para el manejo
    //del endpoints.
    //ApiController identifica a la clase como un controlador en el framework.
    //TallerController se encarga de manejar el endpoint que permite a los usuarios hacer login.
    //Route especifica la ruta para este controlador. En este caso local:
    //http://localhost:5075/api/login
    
    [Route("api/login")]
    [ApiController]
    public class TallerController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public TallerController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        
        // POST api/login
        [HttpPost]
        public ActionResult<AuthResponse> Authenticate(LoginData loginData)
        {

            var auth = _repository.authCheck(loginData);
            if(auth.authenticated){
                return Ok(auth);
            }
            else
            {
                return Unauthorized(auth);
            }
            
            

        }

    }

    //WorkerController Se encarga de manejar operaciones CRUD para los trabajadores registrados.
    //Route especifica la ruta para este controlador. En este caso local:
    //http://localhost:5075/api/manage/worker
    [Route("api/manage/worker")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public WorkerController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        // GET api/manage/worker/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<Trabajador>> GetAllWorkers()
        {

            var allWorkers = _repository.getAllWorkers();
            return Ok(allWorkers);

        }

        // DELETE api/manage/worker
        [HttpDelete]
        public ActionResult<ActionResponse> DeleteWorker(IdRequest deletionId)
        {
            
            var response = _repository.deleteWorker(deletionId);
            return Ok(response);

        }

        // POST api/manage/worker
        [HttpPost]
        public ActionResult<ActionResponse> CreateWorker(Trabajador newWorker)
        {

            var response = _repository.createWorker(newWorker);
            return Ok(response); 

        }

        // PATCH api/manage/worker
        [HttpPatch]
        public ActionResult<ActionResponse> ModifyWorker(Trabajador newWorker)
        {

            var response = _repository.modifyWorker(newWorker);
            return Ok(response); 

        }

    }
}
