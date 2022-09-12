using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{


    //WorkerController Se encarga de manejar operaciones CRUD para los clientes registrados.
    //Route especifica la ruta para este controlador. En este caso local:
    //http://localhost:5075/api/manage/customer
    [Route("api/manage/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public CustomerController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        // GET api/manage/customer/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<Cliente>> GetAllCustomers()
        {

            //not implemented

        }

        // DELETE api/manage/customer
        [HttpDelete]
        public ActionResult<ActionResponse> DeleteCustomer(IdRequest deletionId)
        {
            
            //not implemented

        }

        // POST api/manage/customer
        [HttpPost]
        public ActionResult<ActionResponse> CreateCustomer(Cliente newCustomer)
        {

            //not implemented

        }

        // PATCH api/manage/customer
        [HttpPatch]
        public ActionResult<ActionResponse> ModifyWorker(Cliente newCustomer)
        {

            //not implemented
        }

    }
}
