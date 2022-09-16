using HtmlAgilityPack;
using IronPdf;
using Newtonsoft.Json;
using TallerTECService.Models;

namespace TallerTECService.Data.Billing
{
    public class ReportGenerator
    {
        public static void VehicleReport()
        {
            var vehicleList = new List<VisitPlate>();


            try
            {
                using (StreamReader r = new StreamReader("Data/visitas-placa.json"))
                {
                    string json = r.ReadToEnd();
                    vehicleList = JsonConvert.DeserializeObject<List<VisitPlate>>(json);
                }

                

                HtmlDocument reportDoc = new HtmlDocument();
                reportDoc.Load(@"Data/Reporting/ReportBase.html");

                var fecha = reportDoc.GetElementbyId("fecha");
                fecha.InnerHtml = System.DateTime.Today.ToShortDateString();
                var tipoReporte = reportDoc.GetElementbyId("tipo-reporte");
                tipoReporte.InnerHtml = "REPORTE DE VISITAS POR VEHICULO";
                var tipoVisita = reportDoc.GetElementbyId("tipo-visita");
                tipoVisita.InnerHtml = "Placa de vehiculo";
                var tableBody = reportDoc.GetElementbyId("table-body");

                var sortedList = vehicleList.OrderByDescending(a => a.visitas);
                foreach (var e in sortedList)
                {
                    tableBody.InnerHtml= tableBody.InnerHtml +"<tr><td><span class=\"text-inverse\">"+e.placa+"</span><br></td><td class=\"text-right\">"+e.visitas+"</td></tr>";
                }

                reportDoc.Save(@"Data/Reporting/ReportDoc.html");
                var Renderer = new ChromePdfRenderer();
                var pdf = Renderer.RenderHtmlFileAsPdf("Data/Reporting/ReportDoc.html");
                pdf.SaveAs("Data/Reporting/Generated/report-vehicles.pdf");
                File.Delete("Data/Reporting/ReportDoc.html");

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }


         public static void CustomerReport()
        {
            var customerList = new List<VisitCustomer>();


            try
            {
                using (StreamReader r = new StreamReader("Data/visitas-cliente.json"))
                {
                    string json = r.ReadToEnd();
                    customerList = JsonConvert.DeserializeObject<List<VisitCustomer>>(json);
                }

                

                HtmlDocument reportDoc = new HtmlDocument();
                reportDoc.Load(@"Data/Reporting/ReportBase.html");

                var fecha = reportDoc.GetElementbyId("fecha");
                fecha.InnerHtml = System.DateTime.Today.ToShortDateString();
                var tipoReporte = reportDoc.GetElementbyId("tipo-reporte");
                tipoReporte.InnerHtml = "REPORTE DE VISITAS POR CLIENTE";
                var tipoVisita = reportDoc.GetElementbyId("tipo-visita");
                tipoVisita.InnerHtml = "CEDULA DEL CLIENTE";
                var tableBody = reportDoc.GetElementbyId("table-body");

                var sortedList = customerList.OrderByDescending(a => a.visitas);
                foreach (var e in sortedList)
                {
                    tableBody.InnerHtml= tableBody.InnerHtml +"<tr><td><span class=\"text-inverse\">"+e.id+"</span><br></td><td class=\"text-right\">"+e.visitas+"</td></tr>";
                }

                reportDoc.Save(@"Data/Reporting/ReportDoc.html");
                var Renderer = new ChromePdfRenderer();
                var pdf = Renderer.RenderHtmlFileAsPdf("Data/Reporting/ReportDoc.html");
                pdf.SaveAs("Data/Reporting/Generated/report-customers.pdf");
                File.Delete("Data/Reporting/ReportDoc.html");

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        

    }
}