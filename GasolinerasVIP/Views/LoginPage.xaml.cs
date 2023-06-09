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
            HttpResponseMessage responseMessage = Client.LogIn(new UserLogin { username = user.Text, password = password.Text }).Result;
            if(!responseMessage.IsSuccessStatusCode)
            {
                await DisplayAlert("Datos incorrectos", responseMessage.ReasonPhrase, "ok");
                return;
            }
            await Navigation.PushAsync(new MainPage());
        }
        private async void RegisterBtn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}