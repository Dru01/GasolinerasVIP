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
        private decimal TotalMagna   = 0.0m;
        private decimal TotalPremium = 0.0m;
        private decimal Total = 0.0m;
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
            
            TotalMagna   = Decimal.Parse(MagnaQt.Text) * gasStation.MagnaPrice;
            TotalPremium = Decimal.Parse(PremiumQt.Text) * gasStation.PremiumPrice;

            TotalMagnaPrice.Text   = String.Format("${0:f2}", TotalMagna);
            TotalPremiumPrice.Text = String.Format("${0:f2}", TotalPremium);

            Total = TotalMagna + TotalPremium + 30.00m;
            TotalLbl.Text = String.Format("${0:f2}", Total);
        }
        void Magna_Entry(object sender, EventArgs e)
        {
            MagnaQtDtl.Text = MagnaQt.Text;
            TotalMagna = Decimal.Parse(MagnaQt.Text) * gasStation.MagnaPrice;
            TotalMagnaPrice.Text = String.Format("${0:f2}", TotalMagna);
            Total = TotalMagna + TotalPremium + 30.00m;
            TotalLbl.Text = String.Format("${0:f2}", Total);
        }
        void Premium_Entry(object sender, EventArgs e)
        {
            PremiumQtDtl.Text = PremiumQt.Text;
            TotalPremium = Decimal.Parse(PremiumQt.Text) * gasStation.PremiumPrice;
            TotalPremiumPrice.Text = String.Format("${0:f2}", TotalPremium);
            Total = TotalMagna + TotalPremium + 30.00m;
            TotalLbl.Text = String.Format("${0:f2}", Total);
        }
        private async void Btn_RealizarCompra(object sender, EventArgs e)
        {
            // GenerateOrder
            Transaction transaction = new Transaction()
            {
                GasStationId = gasStation.Id,
                GasStation = gasStation,
                ReceivedOrderDate = DateTime.Now,
                DeliveredOrderDate = DateTime.Now,  
                Status = 1,
                MagnaPrice = gasStation.MagnaPrice,
                OrderedMagna = Decimal.Parse(MagnaQt.Text),
                PremiumPrice = gasStation.PremiumPrice,
                OrderedPremium = Decimal.Parse(PremiumQt.Text),
                ServiceFee = 30.00m,
                DeliveryFee = 20.00m,

                Address = "calle",


            };
            transaction.CalculateBill();
            //Order myOrder = new Order()
            //{
            //    Id= 1,
            //    userId= 1,
            //    Station = gasStation,
            //    OrderedMagna = Decimal.Parse(MagnaQt.Text),
            //    OrderedPremium = Decimal.Parse(PremiumQt.Text),
            //    state= 1,
            //};
            await Navigation.PushAsync(new PaymentPage(transaction));
        }
    }
}