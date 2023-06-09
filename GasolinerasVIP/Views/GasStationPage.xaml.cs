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
        GasStation gasStation;
        Transaction transaction;
        public GasStationPage(GasStation gasStation)
        {
            InitializeComponent();
            this.gasStation = gasStation;
            transaction = new Transaction()
            {
                GasStationId = gasStation.Id,
                GasStation = gasStation,
                ReceivedOrderDate = DateTime.Now,
                DeliveredOrderDate = DateTime.Now,
                Status = 1,
                MagnaPrice = gasStation.MagnaPrice,
                OrderedMagna = 0.00m,
                PremiumPrice = gasStation.PremiumPrice,
                OrderedPremium = 0.00m,
                ServiceFee = 0.00m,
                DeliveryFee = 30.00m,

                Address = "Domicilio",
            };
            GasName.Text  = gasStation.Name;
            GasImg.Source = gasStation.ImageUrl;

            MagnaQtDtl.Text   = MagnaQt.Text;
            PremiumQtDtl.Text = PremiumQt.Text;

            MagnaPrice.Text   = String.Format("${0:f2}", gasStation.MagnaPrice);
            PremiumPrice.Text = String.Format("${0:f2}" ,gasStation.PremiumPrice);

            TotalMagnaPrice.Text   = String.Format("${0:f2}", transaction.MagnaPrice * transaction.OrderedMagna);
            TotalPremiumPrice.Text = String.Format("${0:f2}", transaction.PremiumPrice * transaction.OrderedPremium);


            transaction.CalculateBill();
            TaxLbl.Text = String.Format("${0:f2}", transaction.Tax);
            DescuentoLbl.Text = String.Format("${0:f2}", transaction.Disccount);
            EnvioLbl.Text = String.Format("${0:f2}", transaction.DeliveryFee);
            SubtotalLbl.Text = String.Format("${0:f2}", transaction.Subtotal);
            TotalLbl.Text = String.Format("${0:f2}", transaction.Total);            
        }
        void Magna_Entry(object sender, EventArgs e)
        {
            MagnaQtDtl.Text = MagnaQt.Text;
            transaction.OrderedMagna = Decimal.Parse(MagnaQt.Text);
            TotalMagnaPrice.Text = String.Format("${0:f2}", transaction.MagnaPrice * transaction.OrderedMagna);

            transaction.CalculateBill();
            TaxLbl.Text = String.Format("${0:f2}", transaction.Tax);
            DescuentoLbl.Text = String.Format("${0:f2}", transaction.Disccount);
            EnvioLbl.Text = String.Format("${0:f2}", transaction.DeliveryFee);
            SubtotalLbl.Text = String.Format("${0:f2}", transaction.Subtotal);
            TotalLbl.Text = String.Format("${0:f2}", transaction.Total);            
        }
        void Premium_Entry(object sender, EventArgs e)
        {
            PremiumQtDtl.Text = PremiumQt.Text;
            transaction.OrderedPremium = Decimal.Parse(PremiumQt.Text);
            TotalPremiumPrice.Text = String.Format("${0:f2}", transaction.PremiumPrice * transaction.OrderedPremium);

            transaction.CalculateBill();
            TaxLbl.Text = String.Format("${0:f2}", transaction.Tax);
            DescuentoLbl.Text = String.Format("${0:f2}", transaction.Disccount);
            EnvioLbl.Text = String.Format("${0:f2}", transaction.DeliveryFee);
            SubtotalLbl.Text = String.Format("${0:f2}", transaction.Subtotal);
            TotalLbl.Text = String.Format("${0:f2}", transaction.Total);
        }
       
        private async void Btn_RealizarCompra(object sender, EventArgs e)
        {
            transaction.CalculateBill();
            await Navigation.PushAsync(new PaymentPage(transaction));
        }
    }
}