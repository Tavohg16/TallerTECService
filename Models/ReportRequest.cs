namespace TallerTECService.Models
{
    //Modelo para mensaje Request de creacion de reporte.
    public class ReportRequest
    {
        public int id { get; set; }
        public int dia_inicio { get; set; }
        public int mes_inicio { get; set; }
        public int ano_incio { get; set; }
        public int dia_final { get; set; }
        public int mes_final { get; set; }
        public int ano_final { get; set; }
    }
}