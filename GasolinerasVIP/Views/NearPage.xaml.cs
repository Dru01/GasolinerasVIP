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
        }
        // TODO: Change this with the DB when ready.
        private async void chargeGasStationsDB()
        {
            GasStations = await Client.GetGasStations();
            this.GasStationList.ItemsSource = this.GasStations;

        }

        private async void GasStation_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var gasStation = (GasStation)e.SelectedItem;
            await Navigation.PushAsync(new GasStationPage(gasStation));
        }
    }
}