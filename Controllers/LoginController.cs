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
}