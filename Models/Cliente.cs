namespace TallerTECService.Models
{
    //Modelo de datos de Cliente
    //usado para la gestion de clientes en la solucion, asi como el proceso de autenticacion.
    public class Cliente
    {
        public string? cedula { get; set; }
        public string? usuario {get; set; }
        public string? nombre { get; set; }
        public string? primer_apellido { get; set; }
        public string? segundo_apellido { get; set; }
        public List<string>? telefonos {get; set; }
        public string? contrasena { get; set; }
        public string? correo { get; set; }
        public List<string>? direcciones {get; set; }
        

    }

}