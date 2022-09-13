using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{

    //CustomerController hereda la clase ControllerBase, utilizada para el manejo
    //del endpoints.
    //ApiController identifica a la clase como un controlador en el framework.
    //CustomerController Se encarga de manejar operaciones CRUD para los clientes registrados.
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

            var response = _repository.GetAllCustomers();
            return Ok(response);

        }

        // DELETE api/manage/customer
        [HttpDelete]
        public ActionResult<ActionResponse> DeleteCustomer(IdRequest deletionId)
        {
            
            var response = _repository.DeleteCustomer(deletionId);
            return Ok(response);

        }

        // POST api/manage/customer
        [HttpPost]
        public ActionResult<ActionResponse> CreateCustomer(Cliente newCustomer)
        {

            var response = _repository.CreateCustomer(newCustomer);
            return Ok(response); 

        }

        // PATCH api/manage/customer
        [HttpPatch]
        public ActionResult<ActionResponse> ModifyWorker(Cliente newCustomer)
        {

            var response = _repository.ModifyCustomer(newCustomer);
            return Ok(response); 

        }

    }
}
