using HtmlAgilityPack;
using IronPdf;
using TallerTECService.Models;

namespace TallerTECService.Data.Billing
{
    public class BillGenerator
    {
        
        
        public static void ServiceBill(Appointment app, string custName)
        {
            IronPdf.License.LicenseKey =
             "IRONPDF.KEVINBARRANTESCERDAS.879-F95BAADBEC-DQP7PME7TJLXNHV-74QQER3IHGYM-7XI4HD3SDNMA-NV2FWOKZFMQT-42ZDIABO55TN-SDLYZA-THUAW2COXZ2HUA-DEPLOYMENT.TRIAL-7FBTQF.TRIAL.EXPIRES.14.OCT.2022";

            HtmlDocument billDoc = new HtmlDocument();
            billDoc.Load(@"Data/Billing/BillBase.html");

            var trabajador = billDoc.GetElementbyId("trabajador");
            trabajador.InnerHtml = "Mecanico encargado: " + app.mecanico;
            var cliente = billDoc.GetElementbyId("cliente");
            cliente.InnerHtml = custName;
            var cedula = billDoc.GetElementbyId("cedula");
            cedula.InnerHtml = "Cedula: "+app.cedula_cliente;
            var placa = billDoc.GetElementbyId("placa");
            placa.InnerHtml ="Placa: "+app.placa_vehiculo;
            var id = billDoc.GetElementbyId("id");
            id.InnerHtml = "Factura #: "+ app.id;
            var fecha = billDoc.GetElementbyId("fecha");
            fecha.InnerHtml = app.dia_cita+"/"+app.mes_cita+"/"+app.ano_cita;

            populateBill(app,billDoc);
            
            

            
        }

        private static void populateBill(Appointment app, HtmlDocument billDoc)
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

            if(app.servicio == "aceite")
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

            }

            if(app.servicio == "alineado")
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

            }

            
            billDoc.Save(@"Data/Billing/BillDoc.html");
            var Renderer = new ChromePdfRenderer();
            var pdf = Renderer.RenderHtmlFileAsPdf("Data/Billing/BillDoc.html");
            pdf.SaveAs("Data/Billing/Generated/factura-"+app.id+".pdf");
            File.Delete("Data/Billing/BillDoc.html");
        }

        
    }

}
