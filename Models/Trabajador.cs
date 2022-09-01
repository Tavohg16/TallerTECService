namespace TallerTECService.Models
{
    //Modelo de datos de Trabajador
    //usado para la gestion de trabajadores en la solucion, asi como el proceso de autenticacion.
    public class Trabajador
    {
        public string? nombre { get; set; }
        public string? primer_apellido { get; set; }
        public string? segundo_apellido { get; set; }
        public string? cedula { get; set; }
        public string? contrasena { get; set; }
        public int dia_ingreso { get; set; }
        public int mes_ingreso { get; set; }
        public int ano_ingreso { get; set; }
        public int dia_nacimiento { get; set; }
        public int mes_nacimiento { get; set; }
        public int ano_nacimiento { get; set; }
        public string? rol { get; set; }

    }

}
