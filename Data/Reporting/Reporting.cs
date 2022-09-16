using HtmlAgilityPack;
using IronPdf;
using Newtonsoft.Json;
using TallerTECService.Models;

namespace TallerTECService.Data.Billing
{
    public class ReportGenerator
    {
        public void VehicleReport()
        {
            var saleList = new List<Sale>();


            try
            {
                using (StreamReader r = new StreamReader("Data/ventas.json"))
                {
                    string json = r.ReadToEnd();
                    saleList = JsonConvert.DeserializeObject<List<Sale>>(json);
                }
                

                if (saleList != null)
                {
                    for(int i = 0; i < saleList.Count; i++)
                    {
                        string? placa = saleList[i].placa;
                        for(int j = 0; j < saleList.Count; j++)
                        {
                            
                        }
                        
                    }
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        

    }
}