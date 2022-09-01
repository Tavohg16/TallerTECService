namespace TallerTECService.Models
{
    public class Trabajador
    {
        public string nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public int cedula { get; set; }
        public int dia_ingreso { get; set; }
        public int mes_ingreso { get; set; }
        public int ano_ingreso { get; set; }
        public int dia_nacimiento { get; set; }
        public int mes_nacimiento { get; set; }
        public int ano_nacimiento { get; set; }
        public string rol { get; set; }

    }

}
