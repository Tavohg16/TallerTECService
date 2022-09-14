namespace TallerTECService.Models
{

    //Esta clase es usada como modelo de datos para respuesta de GET request
    //en el que se devuelve una lista de valores
    public class MultivalueCustomer
    {
        public bool exito { get; set; }
        public List<Cliente>? clientes { get; set; }

    }
}
