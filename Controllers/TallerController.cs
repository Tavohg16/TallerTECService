using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{

    //LoginsController hereda la clase ControllerBase, utilizada para el manejo
    //de endpoints.
    //ApiController identifica a la clase como un controlador en el framework
    //Route especifica la ruta para este controlador. En este caso local:
    //http://localhost:5075/api/controller
    
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
            return Ok(auth);

        }

    }
}