namespace TallerTECService.Models
{
    //Modelo para manejo de ventas
    public class Sale
    {
        public string? sucursal { get; set; }
        public int monto { get; set; }
        public string? fecha { get; set; }
        public string? cedula_cliente { get; set; }
        public string? placa { get; set; }
        public string? servicio { get; set; }
        public string? billId { get; set; }
        public bool estado { get; set; }
        
        
    }
}