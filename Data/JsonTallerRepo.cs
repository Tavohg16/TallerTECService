using MlkPwgen;
using Newtonsoft.Json;
using TallerTECService.Models;


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
        public AuthResponse authCheck(LoginData userData)
        {
            var validation = new AuthResponse();
            var workerList = getAllWorkers();
            
            var worker = workerList.AsQueryable().Where(e => e.cedula == userData.cedula).FirstOrDefault();

            if(worker != null && worker.contrasena == userData.contrasena)
            {
            
                validation.authenticated = true;
            }
            else
            {
                validation.authenticated = false;
            }
            
            return validation;

        }

        public ActionResponse createWorker(Trabajador newWorker)
        {
            var response = new ActionResponse();
            var workerList = getAllWorkers();
            var checkId = workerList.AsQueryable().Where(e => e.cedula == newWorker.cedula).FirstOrDefault();

            if(checkId != null)
            {
                response.actualizado=false;
                response.mensaje="Error al crear el trabajador, ya existe un trabajador con la cedula "+checkId.cedula;
                return response;
            }
            
            var checkRole = workerList.AsQueryable().Where(e => e.rol == "Gerente de Sucursal").FirstOrDefault();

            if(checkRole != null && newWorker.rol == "Gerente de Sucursal")
            {
                response.actualizado=false;
                response.mensaje="Error al crear el trabajador, ya existe un gerente de sucursal";
                return response;
            }
            

            workerList.Add(newWorker);
            string json = JsonConvert.SerializeObject(workerList.ToArray());
            System.IO.File.WriteAllText(@"Data/trabajadores.json", json);
            response.actualizado=true;
            response.mensaje="Trabajador creado exitosamente";
            return response;

        }

        public ActionResponse deleteWorker(IdRequest deletionId)
        {
            var response = new ActionResponse();
            var workerList = getAllWorkers();
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
            response.mensaje = "Error al eliminar, no se encontro un trabajador con la cedula "+deletionId.cedula;
            return response;


        }

        public List<Trabajador> getAllWorkers()
        {
            List<Trabajador> workerList = new List<Trabajador>();
            using (StreamReader r = new StreamReader("Data/trabajadores.json"))
            {
                string json = r.ReadToEnd();
                workerList = JsonConvert.DeserializeObject<List<Trabajador>>(json);
            }
            return workerList;
        }

        public ActionResponse modifyWorker(Trabajador modifiedWorker)
        {
            var response = new ActionResponse();
            var workerList = getAllWorkers();
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
            response.mensaje = "Error al modificar, no se encontro un trabajador con la cedula "+ modifiedWorker.cedula;
            return response;

        }

        public List<Cliente> getAllCustomers()
        {
            List<Cliente> customerList = new List<Cliente>();
            using (StreamReader r = new StreamReader("Data/clientes.json"))
            {
                string json = r.ReadToEnd();
                customerList = JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            return customerList;
        }

        public ActionResponse createCustomer(Cliente newCustomer)
        {
            var response = new ActionResponse();
            var customerList = getAllCustomers();
            var checkId = customerList.AsQueryable().Where(e => e.cedula == newCustomer.cedula).FirstOrDefault();

            if(checkId != null)
            {
                response.actualizado=false;
                response.mensaje="Error al crear el cliente, ya existe un trabajador con la cedula "+checkId.cedula;
                return response;
            }
            
            var checkUser = customerList.AsQueryable().Where(e => e.usuario == newCustomer.usuario).FirstOrDefault();

            if(checkUser != null)
            {
                response.actualizado=false;
                response.mensaje="Error al crear el cliente, ya existe el nombre de usuario";
                return response;
            }

            var checkEmail = customerList.AsQueryable().Where(e => e.correo == newCustomer.correo).FirstOrDefault();

            if(checkEmail != null)
            {
                response.actualizado=false;
                response.mensaje="Error al crear el cliente, ya existe un usuario con la direccion de correo brindada";
                return response;
            }
            
            
            newCustomer.contrasena = PasswordGenerator.Generate();
            customerList.Add(newCustomer);
            string json = JsonConvert.SerializeObject(customerList.ToArray());
            System.IO.File.WriteAllText(@"Data/clientes.json", json);
            response.actualizado=true;
            response.mensaje="Cliente creado exitosamente";
            return response;
        }

        
    }
}
