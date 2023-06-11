using GasolinerasVIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasolinerasVIP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyUserPage : ContentPage
    {
        private List<Payment> payments = new List<Payment>();
        private string userId;
        public MyUserPage()
        {
            userId = Client.GetCurrUserId().Result;
            InitializeComponent();
            
            var user = Client.GetCurrUser().Result;
            username.Text = user.Username;
            fullname.Text = user.Fullname;
            email.Text = user.Email;
            phonenumber.Text = user.PhoneNumber;
            if (user.Address == null)
                return;
            string[] address = user.Address.Split(',');
            if (address.Length < 7)
                return;
            street.Text = address[0];
            number.Text = address[1];
            colony.Text = address[2];
            cp.Text = address[3];
            state.Text = address[4];
            city.Text = address[5];
            country.Text = address[6];

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string addres = street.Text + "," + number.Text + "," + colony.Text + "," + cp.Text + "," + state.Text + "," + city.Text + "," + country.Text;
            Console.WriteLine(addres);
            await Client.UpdateCurrUser(new UserInfo { Username = username.Text, Email = email.Text, Fullname = fullname.Text, PhoneNumber = phonenumber.Text, Address = addres });
        }
    }
}