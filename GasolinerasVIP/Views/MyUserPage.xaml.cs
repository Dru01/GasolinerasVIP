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
            
        }
    }
}