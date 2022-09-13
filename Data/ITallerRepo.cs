using TallerTECService.Models;

namespace TallerTECService.Data
{
    //Esta interfaz dicta el contrato basico a seguir para cualquier
    //implementacion del repositorio que maneja la logica de la solucion.
    public interface ITallerRepo
    {
        AuthResponse authCheck(LoginData userData);
        ActionResponse createWorker(Trabajador newWorker);
        List<Trabajador> getAllWorkers();
        List<Cliente> getAllCustomers();
        ActionResponse deleteWorker(IdRequest deletionId);
        ActionResponse createCustomer(Cliente newCustomer);
        ActionResponse modifyWorker(Trabajador modifiedWorker);
        ActionResponse deleteCustomer(IdRequest deletionId);
        ActionResponse modifyCustomer(Cliente modifiedCustomer);
        ActionResponse createAppointment(Appointment newAppointment);
    
        
    }

}
