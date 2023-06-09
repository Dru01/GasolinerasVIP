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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private async void RegisterBtn(object sender, EventArgs e)
        {
            HttpResponseMessage responseMessage = Client.SignUp(new UserInfo { username = user.Text, email = mail.Text, password = password.Text, fullname = name.Text }).Result;
            if(!responseMessage.IsSuccessStatusCode)
            {
                await DisplayAlert("Datos incorrectos", responseMessage.ReasonPhrase, "ok");
                return;
            }
            await Navigation.PushAsync(new LoginPage());
        }
    }
}