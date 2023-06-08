using Newtonsoft.Json;
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
            BaseAddress = new Uri("https://gasvip.azurewebsites.net/api/"),
        };
        public async Task<List<GasStation>> GetGasStations()
        {
            var response = await sharedClient.GetAsync("GasStation");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<GasStation> gasStations = GasStation.FromJson(jsonResponse);
            return gasStations;
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            var response = await sharedClient.GetAsync("Transaction");
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<Transaction> transactions = Transaction.FromJson(jsonResponse);
            return transactions;
        }

        public async Task  PostTransaction(Transaction transaction)
        {
            string jsonString = Serialize.ToJson(transaction);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await sharedClient.PostAsync("Transaction", content);
        }
    }
}
