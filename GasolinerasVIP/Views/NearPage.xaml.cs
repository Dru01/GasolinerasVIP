using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GasolinerasVIP.Models;

namespace GasolinerasVIP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearPage : ContentPage
    {
        public List<GasStation> GasStations = new List<GasStation>();
        public NearPage()
        {
            InitializeComponent();
            chargeGasStationsDB();
            this.GasStationList.ItemsSource = this.GasStations;
        }
        // TODO: Change this with the DB when ready.
        void chargeGasStationsDB()
        {
            GasStation gasStation1 = new GasStation()
            {
                Id = 0,
                ImageUrl = new System.Uri("https://lh3.googleusercontent.com/p/AF1QipNm5gQLj4HK63lxadUYf1adjysbjKdIZDNpNc10=s1360-w1360-h1020"),
                Name = "Oxxo Gas",
                //Location = new Tuple<double, double>(20.996465436160733, -101.28841488455917),
                PremiumPrice = 20.00m,
                MagnaPrice = 17.00m
            };
            GasStation gasStation2 = new GasStation()
            {
                Id = 1,
                ImageUrl = new System.Uri("https://lh3.googleusercontent.com/p/AF1QipO1EJEpwv5_hIUm-G5czSrJ9pp_FGyV4d75qgQI=s1360-w1360-h1020"),
                Name = "Pemex",
                //Location = new Tuple<double, double>(21.010613656306617, -101.27001362367888),
                PremiumPrice = 19.63m,
                MagnaPrice = 17.42m
            };

            this.GasStations.Add(gasStation1);
            this.GasStations.Add(gasStation2);
        }

        private async void GasStation_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var gasStation = (GasStation)e.SelectedItem;
            await Navigation.PushAsync(new GasStationPage(gasStation));
        }
    }
}