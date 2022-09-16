namespace TallerTECService.Models
{
    public class Appointment
    {
        public string? id { get; set; }
        public string? mecanico { get; set; }
        public string? cedula_cliente { get; set; }
        public int dia_cita { get; set; }
        public int mes_cita { get; set; }
        public int ano_cita { get; set; }
        public string? placa_vehiculo { get; set; }
        public string? sucursal { get; set; }
        public string? servicio { get; set; }
        public bool estado { get; set; }
    }
}