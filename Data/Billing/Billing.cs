using HtmlAgilityPack;
using IronPdf;
using Newtonsoft.Json;
using TallerTECService.Models;

namespace TallerTECService.Data.Billing
{
    public class BillGenerator
    {

        public static void RegisterPlate(string plate)
        {
            var plateList = new List<VisitPlate>();


            try
            {
                using (StreamReader r = new StreamReader("Data/visitas-placa.json"))
                {
                    string json = r.ReadToEnd();
                    plateList = JsonConvert.DeserializeObject<List<VisitPlate>>(json);
                }

                var checkId = plateList.AsQueryable().Where(e => e.placa == plate).FirstOrDefault();

                if (checkId == null)
                {
                    var visit = new VisitPlate();
                    visit.placa = plate;
                    visit.visitas = 1;
                    plateList.Add(visit);
                    string newJson = JsonConvert.SerializeObject(plateList.ToArray());
                    System.IO.File.WriteAllText(@"Data/visitas-placa.json", newJson);
                }
                else
                {
                    checkId.visitas++;
                    string newJson = JsonConvert.SerializeObject(plateList.ToArray());
                    System.IO.File.WriteAllText(@"Data/visitas-placa.json", newJson);
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        public static void RegisterCustomer(string customerId)
        {
            var customerList = new List<VisitCustomer>();


            try
            {
                using (StreamReader r = new StreamReader("Data/visitas-cliente.json"))
                {
                    string json = r.ReadToEnd();
                    customerList = JsonConvert.DeserializeObject<List<VisitCustomer>>(json);
                }

                var checkId = customerList.AsQueryable().Where(e => e.id == customerId).FirstOrDefault();

                if (checkId == null)
                {
                    var visit = new VisitCustomer();
                    visit.id = customerId;
                    visit.visitas = 1;
                    customerList.Add(visit);
                    string newJson = JsonConvert.SerializeObject(customerList.ToArray());
                    System.IO.File.WriteAllText(@"Data/visitas-cliente.json", newJson);
                }
                else
                {
                    checkId.visitas++;
                    string newJson = JsonConvert.SerializeObject(customerList.ToArray());
                    System.IO.File.WriteAllText(@"Data/visitas-cliente.json", newJson);
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }


        public static void RegisterSale(Sale newSale)
        {
            var saleList = new List<Sale>();
            var appList = new List<Appointment>();


            try
            {
                using (StreamReader r = new StreamReader("Data/ventas.json"))
                {
                    string json = r.ReadToEnd();
                    saleList = JsonConvert.DeserializeObject<List<Sale>>(json);
                }

                using (StreamReader r = new StreamReader("Data/citas.json"))
                {
                    string json = r.ReadToEnd();
                    appList = JsonConvert.DeserializeObject<List<Appointment>>(json);
                }

                var modifyApp = appList.AsQueryable().Where(e => e.id == newSale.billId).FirstOrDefault();


                if (newSale.estado)
                {
                    newSale.estado = false;
                    modifyApp.estado = false;
                    saleList.Add(newSale);
                    string appJson = JsonConvert.SerializeObject(appList.ToArray());
                    System.IO.File.WriteAllText(@"Data/citas.json", appJson);
                    string newJson = JsonConvert.SerializeObject(saleList.ToArray());
                    System.IO.File.WriteAllText(@"Data/ventas.json", newJson);
                    RegisterPlate(newSale.placa);
                    RegisterCustomer(newSale.cedula_cliente);
                    
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        public static void ServiceBill(Appointment app, string custName)
        {

            HtmlDocument billDoc = new HtmlDocument();
            billDoc.Load(@"Data/Billing/BillBase.html");

            var trabajador = billDoc.GetElementbyId("trabajador");
            trabajador.InnerHtml = "Mecanico encargado: " + app.mecanico;
            var cliente = billDoc.GetElementbyId("cliente");
            cliente.InnerHtml = custName;
            var cedula = billDoc.GetElementbyId("cedula");
            cedula.InnerHtml = "Cedula: " + app.cedula_cliente;
            var placa = billDoc.GetElementbyId("placa");
            placa.InnerHtml = "Placa: " + app.placa_vehiculo;
            var id = billDoc.GetElementbyId("id");
            id.InnerHtml = "Factura #: " + app.id;
            var fecha = billDoc.GetElementbyId("fecha");
            fecha.InnerHtml = app.dia_cita + "/" + app.mes_cita + "/" + app.ano_cita;
            var sucursal = billDoc.GetElementbyId("sucursal");
            sucursal.InnerHtml = "Sucursal " + app.sucursal;

            populateBill(app, billDoc, custName);




        }

        private static void populateBill(Appointment app, HtmlDocument billDoc, string custName)
        {
            var servicio = billDoc.GetElementbyId("servicio");
            var descripcionServicio = billDoc.GetElementbyId("descripcion-servicio");
            var producto = billDoc.GetElementbyId("producto");
            var descripcionProducto = billDoc.GetElementbyId("descripcion-producto");
            var repuesto = billDoc.GetElementbyId("repuesto");
            var descripcionRepuesto = billDoc.GetElementbyId("descripcion-repuesto");
            var precio01 = billDoc.GetElementbyId("precio01");
            var unidades01 = billDoc.GetElementbyId("unidades01");
            var subtotal01 = billDoc.GetElementbyId("subtotal01");
            var precio02 = billDoc.GetElementbyId("precio02");
            var unidades02 = billDoc.GetElementbyId("unidades02");
            var subtotal02 = billDoc.GetElementbyId("subtotal02");
            var precio03 = billDoc.GetElementbyId("precio01");
            var unidades03 = billDoc.GetElementbyId("unidades01");
            var subtotal03 = billDoc.GetElementbyId("subtotal01");
            var total = billDoc.GetElementbyId("total");

            if (app.servicio == "aceite")
            {

                servicio.InnerHtml = "Cambio de aceite";
                descripcionServicio.InnerHtml = "Cambio del aceite de motor y remplazo de filtro";
                producto.InnerHtml = "Aceite Acme";
                descripcionProducto.InnerHtml = "Aceite calidad premium, galon";
                repuesto.InnerHtml = "Filtro de aceite los patitos";
                descripcionRepuesto.InnerHtml = "Filtro de aceite importado";
                precio01.InnerHtml = "₡15,000";
                unidades01.InnerHtml = "1";
                subtotal01.InnerHtml = "₡15,000";
                precio02.InnerHtml = "₡27,950";
                unidades02.InnerHtml = "1";
                subtotal02.InnerHtml = "₡27,950";
                precio03.InnerHtml = "₡8,900";
                unidades03.InnerHtml = "1";
                subtotal03.InnerHtml = "₡8,900";
                total.InnerHtml = "₡38,350";
                var sale = new Sale();
                sale.cedula_cliente = app.cedula_cliente;
                sale.fecha = app.dia_cita + "/" + app.mes_cita + "/" + app.ano_cita;
                sale.monto = 38350;
                sale.placa = app.placa_vehiculo;
                sale.servicio = app.servicio;
                sale.sucursal = app.sucursal;
                sale.billId = app.id;
                sale.estado = app.estado;
                RegisterSale(sale);


            }

            if (app.servicio == "alineado")
            {

                servicio.InnerHtml = "Alineado y cambio de llantas";
                descripcionServicio.InnerHtml = "Alineado de llantas y sustitucion por desgaste";
                producto.InnerHtml = "LLantas Michelin";
                descripcionProducto.InnerHtml = "Llanta premium";
                repuesto.InnerHtml = "Disco de freno";
                descripcionRepuesto.InnerHtml = "Disco de freno perforado";
                precio01.InnerHtml = "₡18,000";
                unidades01.InnerHtml = "1";
                subtotal01.InnerHtml = "₡18,000";
                precio02.InnerHtml = "₡104,000";
                unidades02.InnerHtml = "4";
                subtotal02.InnerHtml = "416,000";
                precio03.InnerHtml = "₡55,000";
                unidades03.InnerHtml = "1";
                subtotal03.InnerHtml = "₡55.000";
                total.InnerHtml = "₡489,000";
                var sale = new Sale();
                sale.cedula_cliente = app.cedula_cliente;
                sale.fecha = app.dia_cita + "/" + app.mes_cita + "/" + app.ano_cita;
                sale.monto = 489000;
                sale.placa = app.placa_vehiculo;
                sale.servicio = app.servicio;
                sale.sucursal = app.sucursal;
                sale.billId = app.id;
                RegisterSale(sale);

            }


            billDoc.Save(@"Data/Billing/BillDoc.html");
            var Renderer = new ChromePdfRenderer();
            var pdf = Renderer.RenderHtmlFileAsPdf("Data/Billing/BillDoc.html");
            pdf.SaveAs("Data/Billing/Generated/factura-" + app.id + ".pdf");
            File.Delete("Data/Billing/BillDoc.html");
        }



    }



}
