using GasolinerasVIP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasolinerasVIP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void LogInBtn(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var url = "https://gasolinerasvip.azurewebsites.net/Users/Login";
            UserLogin userLogin = new UserLogin { username = user.Text, password = password.Text };
            StringContent content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                await DisplayAlert("Datos incorrectos", responseMessage.ReasonPhrase, "ok");
                return;
            }
            var json = responseMessage.Content.ReadAsStringAsync().Result;
            UserToken userToken = JsonConvert.DeserializeObject<UserToken>(json);

            await SecureStorage.SetAsync("token", userToken.token);

            await Navigation.PushAsync(new MainPage(1));
        }
        private async void RegisterBtn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}