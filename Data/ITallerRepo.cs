using TallerTECService.Models;

namespace TallerTECService.Data
{
    //Esta interfaz dicta el contrato basico a seguir para cualquier
    //implementacion del repositorio que maneja la logica de la solucion.
    public interface ITallerRepo
    {
        AuthResponse AuthCheck(LoginData userData);
        ActionResponse CreateWorker(Trabajador newWorker);
        List<Trabajador> GetAllWorkers();
        List<Cliente> GetAllCustomers();
        ActionResponse DeleteWorker(IdRequest deletionId);
        ActionResponse CreateCustomer(Cliente newCustomer);
        ActionResponse ModifyWorker(Trabajador modifiedWorker);
        ActionResponse DeleteCustomer(IdRequest deletionId);
        ActionResponse ModifyCustomer(Cliente modifiedCustomer);
        ActionResponse CreateAppointment(Appointment newAppointment);
        List<Appointment> GetAllAppointments();
    
        
    }

}
