using MlkPwgen;
using Newtonsoft.Json;
using TallerTECService.Models;
using TallerTECService.Coms;
using TallerTECService.Data.Billing;


namespace TallerTECService.Data
{
    //Implementacion de la logica para cada una de los ActionResult en TallerController,
    //esta clase extiende la interfaz ILoginRepo, e implementa los metodos relacionados
    //a la manipulacion de datos necesaria para cumplir con los requerimientos funcionales
    //de la aplicacion.
    public class JsonTallerRepo : ITallerRepo
    {


        //Logica de autenticacion de usuarios. Hace uso de libreria NewtonSoft.Json para el manejo de la base de datos.
        //Recibe una instancia de LoginData creada con el mensaje entrante desde el cliente en el endpoint POST api/login
        //Retorna una instancia de AuthResponse. 
        public AuthResponse AuthCheck(LoginData userData)
        {
            var validation = new AuthResponse();
            var workerList = GetAllWorkers().trabajadores;

            var worker = workerList.AsQueryable().Where(e => e.cedula == userData.cedula).FirstOrDefault();

            if (worker != null && worker.contrasena == userData.contrasena)
            {

                validation.authenticated = true;
            }
            else
            {
                validation.authenticated = false;
            }

            return validation;

        }

        public ActionResponse CreateWorker(Trabajador newWorker)
        {
            var response = new ActionResponse();
            var workerList = GetAllWorkers().trabajadores;
            var checkId = workerList.AsQueryable().Where(e => e.cedula == newWorker.cedula).FirstOrDefault();

            if (checkId != null)
            {
                response.actualizado = false;
                response.mensaje = "Error al crear el trabajador, ya existe un trabajador con la cedula " + checkId.cedula;
                return response;
            }

            var checkRole = workerList.AsQueryable().Where(e => e.rol == "Gerente de Sucursal").FirstOrDefault();

            if (checkRole != null && newWorker.rol == "Gerente de Sucursal")
            {
                response.actualizado = false;
                response.mensaje = "Error al crear el trabajador, ya existe un gerente de sucursal";
                return response;
            }


            newWorker.rol = newWorker.rol.ToLower();
            workerList.Add(newWorker);
            string json = JsonConvert.SerializeObject(workerList.ToArray());
            System.IO.File.WriteAllText(@"Data/trabajadores.json", json);
            response.actualizado = true;
            response.mensaje = "Trabajador creado exitosamente";
            return response;

        }

        public ActionResponse DeleteWorker(IdRequest deletionId)
        {
            var response = new ActionResponse();
            var workerList = GetAllWorkers().trabajadores;
            var itemToDelete = workerList.SingleOrDefault(e => e.cedula == deletionId.cedula);

            if (itemToDelete != null)
            {
                workerList.Remove(itemToDelete);
                string json = JsonConvert.SerializeObject(workerList.ToArray());
                System.IO.File.WriteAllText(@"Data/trabajadores.json", json);
                response.actualizado = true;
                response.mensaje = "Trabajador eliminado exitosamente";
                return response;
            }

            response.actualizado = false;
            response.mensaje = "Error al eliminar, no se encontro un trabajador con la cedula " + deletionId.cedula;
            return response;


        }

        public MultivalueWorker GetAllWorkers()
        {
            var workerList = new List<Trabajador>();
            var response = new MultivalueWorker();

            try
            {
                using (StreamReader r = new StreamReader("Data/trabajadores.json"))
                {
                string json = r.ReadToEnd();
                workerList = JsonConvert.DeserializeObject<List<Trabajador>>(json);
                }

                response.trabajadores = workerList;
                response.exito = true;
                return response;

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                response.exito = false;
                response.trabajadores = new List<Trabajador>(0);
                return response;
            }
            
            
        }

        public ActionResponse ModifyWorker(Trabajador modifiedWorker)
        {
            var response = new ActionResponse();
            var workerList = GetAllWorkers().trabajadores;
            var itemToModify = workerList.SingleOrDefault(e => e.cedula == modifiedWorker.cedula);

            if (itemToModify != null)
            {

                modifiedWorker.rol = modifiedWorker.rol.ToLower();
                
                if(modifiedWorker.rol == "gerente de sucursal")
                {

                    var gerente = workerList.SingleOrDefault(e => e.rol == modifiedWorker.rol);

                    if(gerente != null)
                    {
                        response.actualizado = false;
                        response.mensaje = "Error al modificar, ya existe un gerente de sucursal ";
                        return response;
                    }
                }
                workerList.Remove(itemToModify);
                workerList.Add(modifiedWorker);
                string json = JsonConvert.SerializeObject(workerList.ToArray());
                System.IO.File.WriteAllText(@"Data/trabajadores.json", json);
                response.actualizado = true;
                response.mensaje = "Trabajador modificado exitosamente";
                return response;
            }

            response.actualizado = false;
            response.mensaje = "Error al modificar, no se encontro un trabajador con la cedula " + modifiedWorker.cedula;
            return response;

        }

        

        public MultivalueCustomer GetAllCustomers()
        {
            var customerList = new List<Cliente>();
            var response = new MultivalueCustomer();

            try
            {
                using (StreamReader r = new StreamReader("Data/clientes.json"))
                {
                    string json = r.ReadToEnd();
                    customerList = JsonConvert.DeserializeObject<List<Cliente>>(json);
                }

                response.exito = true;
                response.clientes = customerList;
                return response;
            }
            catch(FileNotFoundException e)
            {
                response.exito = false;
                response.clientes = new List<Cliente>(0);
                Console.WriteLine("ERROR: " + e.Message);
                return response;
            }
            
        }

        public Cliente GetCustomer(IdRequest customerId)
        {
            var customerList = GetAllCustomers().clientes;
            var customer = customerList.SingleOrDefault(e => e.cedula == customerId.cedula);

            if (customer != null)
            {
                return customer;
            }

            var invalidCustomer = new Cliente();
            invalidCustomer.cedula = "NOT FOUND";
            return invalidCustomer;

        }

        public ActionResponse CreateCustomer(Cliente newCustomer)
        {
            var response = new ActionResponse();
            var customerList = GetAllCustomers().clientes;
            var checkId = customerList.AsQueryable().Where(e => e.cedula == newCustomer.cedula).FirstOrDefault();

            if (checkId != null)
            {
                response.actualizado = false;
                response.mensaje = "Error al crear el cliente, ya existe un cliente con la cedula " + checkId.cedula;
                return response;
            }

            var checkUser = customerList.AsQueryable().Where(e => e.usuario == newCustomer.usuario).FirstOrDefault();

            if (checkUser != null)
            {
                response.actualizado = false;
                response.mensaje = "Error al crear el cliente, ya existe el nombre de usuario";
                return response;
            }

            var checkEmail = customerList.AsQueryable().Where(e => e.correo == newCustomer.correo).FirstOrDefault();

            if (checkEmail != null)
            {
                response.actualizado = false;
                response.mensaje = "Error al crear el cliente, ya existe un usuario con la direccion de correo brindada";
                return response;
            }


            newCustomer.contrasena = PasswordGenerator.Generate();
            customerList.Add(newCustomer);
            string json = JsonConvert.SerializeObject(customerList.ToArray());
            System.IO.File.WriteAllText(@"Data/clientes.json", json);
            response.actualizado=true;
            response.mensaje="Cliente creado exitosamente";
            EmailSender.SendCreationEmail(newCustomer);
            return response;
        }



        public ActionResponse DeleteCustomer(IdRequest deletionId)
        {
            var response = new ActionResponse();
            var customerList = GetAllCustomers().clientes;
            var itemToDelete = customerList.SingleOrDefault(e => e.cedula == deletionId.cedula);

            if (itemToDelete != null)
            {
                customerList.Remove(itemToDelete);
                string json = JsonConvert.SerializeObject(customerList.ToArray());
                System.IO.File.WriteAllText(@"Data/clientes.json", json);
                response.actualizado = true;
                response.mensaje = "Cliente eliminado exitosamente";
                return response;
            }

            response.actualizado = false;
            response.mensaje = "Error al eliminar, no se encontro un cliente con la cedula " + deletionId.cedula;
            return response;
        }

        public ActionResponse ModifyCustomer(Cliente modifiedCustomer)
        {
            var response = new ActionResponse();
            var customerList = GetAllCustomers().clientes;
            var itemToModify = customerList.SingleOrDefault(e => e.cedula == modifiedCustomer.cedula);

            if (itemToModify != null)
            {
                customerList.Remove(itemToModify);
                customerList.Add(modifiedCustomer);
                string json = JsonConvert.SerializeObject(customerList.ToArray());
                System.IO.File.WriteAllText(@"Data/clientes.json", json);
                response.actualizado = true;
                response.mensaje = "Cliente modificado exitosamente";
                return response;
            }

            response.actualizado = false;
            response.mensaje = "Error al modificar, no se encontro un cliente con la cedula " + modifiedCustomer.cedula;
            return response;
        }

        public MultivalueApp GetAllAppointments()
        {
            var appList = new List<Appointment>();
            var response = new MultivalueApp();

            try
            {
                using (StreamReader r = new StreamReader("Data/citas.json"))
                {
                    string json = r.ReadToEnd();
                    appList = JsonConvert.DeserializeObject<List<Appointment>>(json);
                }

                response.exito = true;
                response.citas = appList;
                return response;
            }
            catch(FileNotFoundException e)
            {
                response.exito = false;
                response.citas = new List<Appointment>(0);
                Console.WriteLine("ERROR: "+e.Message);
                return response;
            }

            
        }

        public ActionResponse CreateAppointment(Appointment newAppointment)
        {
            var response = new ActionResponse();
            var appList = GetAllAppointments().citas;
            var customerList = GetAllCustomers().clientes;
            var workerList = GetAllWorkers().trabajadores;


            var checkId = customerList.AsQueryable().Where(e => e.cedula == newAppointment.cedula_cliente).FirstOrDefault();

            if (checkId != null)
            {
                var checkBillId = appList.AsQueryable().Where(e => e.id == newAppointment.id).FirstOrDefault();

                if (checkBillId == null)
                {
                    var mechanics = new List<Trabajador>();
                    for (int i = 0; i < workerList.Count; i++)
                    {
                        var worker = workerList.ElementAt<Trabajador>(i);

                        if (worker.rol == "Mecanico")
                        {
                            mechanics.Add(worker);
                        }
                    }

                    var random = new System.Random();
                    int index = random.Next(0, mechanics.Count);
                    var name = mechanics.ElementAt<Trabajador>(index).nombre;
                    var lname01 = mechanics.ElementAt<Trabajador>(index).primer_apellido;
                    var lname02 = mechanics.ElementAt<Trabajador>(index).segundo_apellido;
                    newAppointment.mecanico = name + " " + lname01 + " " + lname02;
                    appList.Add(newAppointment);
                    string json = JsonConvert.SerializeObject(appList.ToArray());
                    System.IO.File.WriteAllText(@"Data/citas.json", json);
                    response.actualizado = true;
                    response.mensaje = "Cita creada exitosamente";
                    return response;

                }
                else
                {
                    response.actualizado = false;
                    response.mensaje = "Error al crear la cita, ya existe una factura con el id "+ newAppointment.id;
                    return response;
                }

            }
            else
            {
                response.actualizado = false;
                response.mensaje = "Error al crear la cita, no existe un cliente con la cedula " + newAppointment.cedula_cliente;
                return response;

            }

        }


        public ActionResponse CreateBill(BillRequest newBill)
        {
            var response = new ActionResponse();
            var appList = GetAllAppointments().citas;
            var checkId = appList.AsQueryable().Where(e => e.id == newBill.id).FirstOrDefault();

            if (checkId != null)
            {
                
                var idRequest = new IdRequest();
                idRequest.cedula = checkId.cedula_cliente;
                Cliente customer = GetCustomer(idRequest);
                string custName =  customer.nombre+" "+customer.primer_apellido+" "+
                customer.segundo_apellido;
                BillGenerator.ServiceBill(checkId,custName);
                response.actualizado = true;
                response.mensaje = "Factura creada exitosamente";
                return response;
            }

            response.actualizado = false;
            response.mensaje = "Error al crear factura";
            return response;
        }

        public ActionResponse CreateReport(ReportRequest newReport)
        {
            var response = new ActionResponse();

            if(newReport.id ==1)
            {
                ReportGenerator.SalesReport(newReport);
            }

            if(newReport.id == 2)
            {
                ReportGenerator.VehicleReport();
            }
            
            if(newReport.id == 3)
            {
                ReportGenerator.CustomerReport();
            }
            
            
            response.actualizado = true;
            response.mensaje = "EN CONSTRUCCION, PDF GENERADO EN PROYECTO TallerTECService";
            return response;
            
        }
    }

}


