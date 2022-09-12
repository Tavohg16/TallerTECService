using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{


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
