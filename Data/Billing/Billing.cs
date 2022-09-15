using IronPdf;
using TallerTECService.Models;

namespace TallerTECService.Data.Billing
{
    public static class BillGenerator
    {
        public static void ServiceBill(Appointment app)
        {
            IronPdf.License.LicenseKey =
             "IRONPDF.KEVINBARRANTESCERDAS.879-F95BAADBEC-DQP7PME7TJLXNHV-74QQER3IHGYM-7XI4HD3SDNMA-NV2FWOKZFMQT-42ZDIABO55TN-SDLYZA-THUAW2COXZ2HUA-DEPLOYMENT.TRIAL-7FBTQF.TRIAL.EXPIRES.14.OCT.2022";

            var Renderer = new ChromePdfRenderer();
            var pdf = Renderer.RenderUrlAsPdf("Data/Billing/BillBase.html");
            pdf.SaveAs("Data/Billing/Generated/factura-"+app.id+".pdf");
        }
    }

}
