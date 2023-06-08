using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GasolinerasVIP.Models
{
   
    public class Client
    {
        private static HttpClient sharedClient = new HttpClient()
        { 
            BaseAddress = new Uri("https://gasvip.azurewebsites.net/api/GasStation"),
        };
        public static async Task<List<GasStation>> GetGasStations()
        {
            var response = await sharedClient.GetAsync("GasStation");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<GasStation> gasStations = GasStation.FromJson(jsonResponse);
            return gasStations;
        }
    }
}
