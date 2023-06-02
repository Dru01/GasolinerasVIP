using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GasolinerasVIP.Models;
using System.Numerics;

namespace GasolinerasVIP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GasStationPage : ContentPage
    {
        private double TotalMagna   = 0.0;
        private double TotalPremium = 0.0;
        private double Total = 0.0;
        GasStation gasStation;
        public GasStationPage(GasStation gasStation)
        {
            InitializeComponent();
            this.gasStation = gasStation;
            GasName.Text  = gasStation.Name;
            GasImg.Source = gasStation.ImageUrl;
            
            MagnaQtDtl.Text   = MagnaQt.Text;
            PremiumQtDtl.Text = PremiumQt.Text;
            
            MagnaPrice.Text   = String.Format("${0:f2}", gasStation.MagnaPrice);
            PremiumPrice.Text = String.Format("${0:f2}" ,gasStation.PremiumPrice);
            
            TotalMagna   = Double.Parse(MagnaQt.Text) * gasStation.MagnaPrice;
            TotalPremium = Double.Parse(PremiumQt.Text) * gasStation.PremiumPrice;

            TotalMagnaPrice.Text   = String.Format("${0:f2}", TotalMagna);
            TotalPremiumPrice.Text = String.Format("${0:f2}", TotalPremium);

            Total = TotalMagna + TotalPremium + 30.00;
            TotalLbl.Text = String.Format("${0:f2}", Total);
        }
        void Magna_Entry(object sender, EventArgs e)
        {
            MagnaQtDtl.Text = MagnaQt.Text;
            TotalMagna = Double.Parse(MagnaQt.Text) * gasStation.MagnaPrice;
            TotalMagnaPrice.Text = String.Format("${0:f2}", TotalMagna);
            Total = TotalMagna + TotalPremium + 30.00;
            TotalLbl.Text = String.Format("${0:f2}", Total);
        }
        void Premium_Entry(object sender, EventArgs e)
        {
            PremiumQtDtl.Text = PremiumQt.Text;
            TotalPremium = Double.Parse(PremiumQt.Text) * gasStation.PremiumPrice;
            TotalPremiumPrice.Text = String.Format("${0:f2}", TotalPremium);
            Total = TotalMagna + TotalPremium + 30.00;
            TotalLbl.Text = String.Format("${0:f2}", Total);
        }
        private async void Btn_RealizarCompra(object sender, EventArgs e)
        {
            // GenerateOrder
            Order myOrder = new Order()
            {
                Id= 1,
                userId= 1,
                Station = gasStation,
                OrderedMagna = Double.Parse(MagnaQt.Text),
                OrderedPremium = Double.Parse(PremiumQt.Text),
                state= 1,
            };
            await Navigation.PushAsync(new PaymentPage(myOrder));
        }
    }
}