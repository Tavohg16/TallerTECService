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
                    tableBody.InnerHtml = tableBody.InnerHtml + "<tr><td><span class=\"text-inverse\">" + e.placa + "</span><br></td><td class=\"text-right\">" + e.visitas + "</td></tr>";
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
                    tableBody.InnerHtml = tableBody.InnerHtml + "<tr><td><span class=\"text-inverse\">" + e.id + "</span><br></td><td class=\"text-right\">" + e.visitas + "</td></tr>";
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


        public static void SalesReport(ReportRequest request)
        {
            var saleList = new List<Sale>();
            var sanJose = new List<Sale>();
            var cartago = new List<Sale>();
            var alajuela = new List<Sale>();
            var totals = new List<int>();


            try
            {
                using (StreamReader r = new StreamReader("Data/ventas.json"))
                {
                    string json = r.ReadToEnd();
                    saleList = JsonConvert.DeserializeObject<List<Sale>>(json);
                }

                
                sanJose.AddRange(saleList.FindAll(e => e.sucursal.ToLower() == "san josé"));
                cartago.AddRange(saleList.FindAll(e => e.sucursal.ToLower() == "cartago"));
                alajuela.AddRange(saleList.FindAll(e => e.sucursal.ToLower() == "alajuela"));

                string startString = request.dia_inicio+"/"+request.mes_inicio+"/"+request.ano_incio;
                string endString = request.dia_final+"/"+request.mes_final+"/"+request.ano_final;
                var startDate = Convert.ToDateTime(startString);
                var endDate = Convert.ToDateTime(endString);

                var sanJoseInRange = SalesByDate(sanJose,startDate,endDate).OrderByDescending(e => e.monto);
                var cartagoInRange = SalesByDate(cartago,startDate,endDate).OrderByDescending(e => e.monto);
                var alajuelaInRange = SalesByDate(alajuela,startDate,endDate).OrderByDescending(e => e.monto);
                
                
                int totalSanJose = GetTotal(sanJoseInRange);
                int totalCartago = GetTotal(cartagoInRange);
                int totalAlajuela = GetTotal(alajuelaInRange);


                HtmlDocument reportDoc = new HtmlDocument();
                reportDoc.Load(@"Data/Reporting/ReportBase.html");

                var fecha = reportDoc.GetElementbyId("fecha");
                fecha.InnerHtml = System.DateTime.Today.ToShortDateString();
                var tableBody = reportDoc.GetElementbyId("table-body");
                var tipoReporte = reportDoc.GetElementbyId("tipo-reporte");
                tipoReporte.InnerHtml = "REPORTE DE VENTAS POR SUCURSAL";
                var tipoVisita = reportDoc.GetElementbyId("tipo-visita");
                tipoVisita.InnerHtml = "SUCURSAL";
                var tipoDato = reportDoc.GetElementbyId("tipo-dato");
                tipoDato.InnerHtml = "MONTO DE VENTA EN COLONES";
                var total01 = reportDoc.GetElementbyId("total02");
                var total02 = reportDoc.GetElementbyId("total03");
                var total03 = reportDoc.GetElementbyId("total01");

                total01.InnerHtml = totalSanJose.ToString();
                total02.InnerHtml = totalAlajuela.ToString();
                total03.InnerHtml = totalCartago.ToString();

                WriteRows(sanJoseInRange,tableBody);
                WriteRows(cartagoInRange,tableBody);
                WriteRows(alajuelaInRange,tableBody);

                reportDoc.Save(@"Data/Reporting/ReportDoc.html");
                var Renderer = new ChromePdfRenderer();
                var pdf = Renderer.RenderHtmlFileAsPdf("Data/Reporting/ReportDoc.html");
                pdf.SaveAs("Data/Reporting/Generated/report-sales.pdf");
                File.Delete("Data/Reporting/ReportDoc.html");


            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        private static void WriteRows(IOrderedEnumerable<Sale> list, HtmlNode tableBody)
        {
            foreach (var e in list)
            {
                tableBody.InnerHtml = tableBody.InnerHtml = tableBody.InnerHtml + "<tr><td><span class=\"text-inverse\">" + e.sucursal + "</span><br></td><td class=\"text-center\">"+e.billId+"</td><td class=\"text-right\">" + e.monto + "</td></tr>";
            }
        }

        private static int GetTotal(IOrderedEnumerable<Sale> location)
        {
            int total = 0;

            foreach (var e in location)
                {
                    total = total + e.monto;
                }
            
            return total;
        }


        private static List<Sale> SalesByDate(List<Sale> location,DateTime start,DateTime end)
        {
            var inRange = new List<Sale>();

            foreach (var e in location)
            {
                var saleDateString = e.fecha;
                var saleDate = Convert.ToDateTime(saleDateString);

                if(start.Date <= saleDate.Date && saleDate.Date <= end.Date)
                {
                    inRange.Add(e);
                }
            }

            return inRange;

    
        }

    }
}