
namespace TallerTECService.Models
{
    //Modelo de mensaje entrante desde frontend para intento de login
    //se recibira 3 strings correspondientes al tipo de usuario, nombre de 
    //usuario y contrasena.
    public class LoginData
    {
        public string? nombreUsuario { get; set; }
        public string? contrasena { get; set; }
    }
}
