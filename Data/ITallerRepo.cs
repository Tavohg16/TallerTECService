using TallerTECService.Models;

namespace TallerTECService.Data
{
    //Esta interfaz dicta el contrato basico a seguir para cualquier
    //implementacion del repositorio que maneja la logica de la solucion.
    public interface ITallerRepo
    {
        AuthResponse AuthCheck(LoginData userData);
        ActionResponse CreateWorker(Trabajador newWorker);
        MultivalueWorker GetAllWorkers();
        ActionResponse ModifyWorker(Trabajador modifiedWorker);
        ActionResponse DeleteWorker(IdRequest deletionId);
        ActionResponse CreateCustomer(Cliente newCustomer);
        Cliente GetCustomer(IdRequest customerId);
        MultivalueCustomer GetAllCustomers();
        ActionResponse DeleteCustomer(IdRequest deletionId);
        ActionResponse ModifyCustomer(Cliente modifiedCustomer);
        ActionResponse CreateAppointment(Appointment newAppointment);
        MultivalueApp GetAllAppointments();
        ActionResponse CreateBill(BillRequest newBill);
        ActionResponse CreateReport(ReportRequest newReport);
    
    }

}
