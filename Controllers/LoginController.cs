using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TallerTECService.Data;
using TallerTECService.Models;

namespace TallerTECService.Controllers
{
    //LoginController hereda la clase ControllerBase, utilizada para el manejo
    //del endpoints.
    //ApiController identifica a la clase como un controlador en el framework.
    //LoginController se encarga de manejar el endpoint que permite a los usuarios hacer login.
    //Route especifica la ruta para este controlador. En este caso local:
    //http://localhost:5075/api/login
    
    [Route("api/login")]
    [ApiController]
    [EnableCors("Policy")]
    public class LoginController : ControllerBase
    {
        private readonly ITallerRepo _repository;

        public LoginController(ITallerRepo repository)
        {
            _repository = repository;
        }
        
        
        // POST api/login
        [HttpPost]
        public ActionResult<AuthResponse> Authenticate(LoginData loginData)
        {

            var response = _repository.AuthCheck(loginData);
            if(response.authenticated){
                return Ok(response);
            }
            else
            {
                return Unauthorized(response);
            }
            
            

        }

    }
}