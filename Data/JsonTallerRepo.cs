using MlkPwgen;
using Newtonsoft.Json;
using TallerTECService.Models;
using TallerTECService.Coms;


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
            var workerList = GetAllWorkers();

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
            var workerList = GetAllWorkers();
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
            var workerList = GetAllWorkers();
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

        public List<Trabajador> GetAllWorkers()
        {
            List<Trabajador> workerList = new List<Trabajador>();
            using (StreamReader r = new StreamReader("Data/trabajadores.json"))
            {
                string json = r.ReadToEnd();
                workerList = JsonConvert.DeserializeObject<List<Trabajador>>(json);
            }
            return workerList;
        }

        public ActionResponse ModifyWorker(Trabajador modifiedWorker)
        {
            var response = new ActionResponse();
            var workerList = GetAllWorkers();
            var itemToModify = workerList.SingleOrDefault(e => e.cedula == modifiedWorker.cedula);

            if (itemToModify != null)
            {
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

        public List<Cliente> GetAllCustomers()
        {
            List<Cliente> customerList = new List<Cliente>();
            using (StreamReader r = new StreamReader("Data/clientes.json"))
            {
                string json = r.ReadToEnd();
                customerList = JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            return customerList;
        }

        public ActionResponse CreateCustomer(Cliente newCustomer)
        {
            var response = new ActionResponse();
            var customerList = GetAllCustomers();
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
            var customerList = GetAllCustomers();
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
            var customerList = GetAllCustomers();
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
            var customerList = GetAllCustomers();
            var checkId = customerList.AsQueryable().Where(e => e.cedula == newAppointment.cedula_cliente).FirstOrDefault();

            if (checkId != null)
            {
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
                response.mensaje = "Error al crear la cita, no existe un cliente con la cedula " + newAppointment.cedula_cliente;
                return response;

            }

        }




    }

}


