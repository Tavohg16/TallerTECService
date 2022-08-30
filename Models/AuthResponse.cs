namespace TallerTECService.Models
{
    //Modelo de respuesta a frontend para intento de login,
    //Si la autenticacion es exitosa, la propiedad authentication sera true,
    //de lo contrario, false.
    public class AuthResponse
    {
        
        public bool authenticated { get; set; }
    }
    

}