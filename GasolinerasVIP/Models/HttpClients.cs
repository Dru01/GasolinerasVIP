using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        public async Task<List<Transaction>> GetUserTransactions(string userId)
        {
            var response = await sharedClient.GetAsync("Transaction/userid/" + userId);
            var jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<Transaction> transactions = Transaction.FromJson(jsonResponse);
            return transactions;
        }

        public async Task PostTransaction(Transaction transaction)
        {
            string jsonString = Serialize.ToJson(transaction);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await sharedClient.PostAsync("Transaction", content);
        }

        public static async void save_token(Token token)
        {
            await SecureStorage.SetAsync("Access_Token", token.Access_Token);
            await SecureStorage.SetAsync("Refresh_Token", token.Refresh_Token);
            sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Access_Token);
        }

        public static async Task<HttpResponseMessage> LogIn(UserLogin user)
        {
            HttpResponseMessage responseMessage = await sharedClient.PostAsync(
                "Users/Login",
                new StringContent(JsonConvert.SerializeObject(user),
                    Encoding.UTF8,
                    "application/json"
                )
            ).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
                save_token(JsonConvert.DeserializeObject<Token>(responseMessage.Content.ReadAsStringAsync().Result));
            return responseMessage;

        }

        public static async Task<HttpResponseMessage> SignUp(UserInfo user)
        {
            return await sharedClient.PostAsync(
                "Users/SignUp",
                new StringContent(JsonConvert.SerializeObject(user),
                    Encoding.UTF8,
                    "application/json"
                )
            ).ConfigureAwait(false);
        }

        public static async Task<string> GetCurrUserId()
        {
            var response = await sharedClient.GetAsync("Users/CurrUserId").ConfigureAwait(false);
            return response.Content.ReadAsStringAsync().Result;
        }

        public static bool CheckTokenStatus(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

        public async static Task<TaskStatus> RefreshToken()
        {
            var Acces_Token = SecureStorage.GetAsync("Access_Token");
            if (Acces_Token.Result == null)
                return Acces_Token.Status;
            if (CheckTokenStatus(Acces_Token.Result))
                return TaskStatus.RanToCompletion;
            
            var Refresh_Token = SecureStorage.GetAsync("Refresh_Token");
            if (Refresh_Token.Result == null)
                return Refresh_Token.Status;
            
            HttpResponseMessage responseMessage = await sharedClient.PostAsync(
                "Users/Refresh",
                new StringContent
                (
                    JsonConvert.SerializeObject(new Token
                        {
                            Access_Token = Acces_Token.Result,
                            Refresh_Token = Refresh_Token.Result
                        }
                    ),
                    Encoding.UTF8,
                    "application/json"
                )
            ).ConfigureAwait(false);
            if (!responseMessage.IsSuccessStatusCode)
                return TaskStatus.Canceled;
            save_token(JsonConvert.DeserializeObject<Token>(responseMessage.Content.ReadAsStringAsync().Result));
            return TaskStatus.RanToCompletion;
        }

    }
}
