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
    }
}
