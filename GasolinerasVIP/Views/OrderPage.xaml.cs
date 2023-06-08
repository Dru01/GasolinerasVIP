using GasolinerasVIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasolinerasVIP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderPage : ContentPage
	{
		public OrderPage (Transaction myTransaction)
		{
			InitializeComponent();
			DiaDeRegistro.Text = String.Format("Dia de registro: {0:M}", myTransaction.ReceivedOrderDate);
			LugarDeEstacion.Text = String.Format("Lugar: {0}", myTransaction.GasStation.Name);

			MagnaQtDtl.Text   = String.Format("{0}", myTransaction.OrderedMagna);
			PremiumQtDtl.Text = String.Format("{0}", myTransaction.OrderedPremium);

            MagnaPrice.Text   = String.Format("${0:f2}", myTransaction.MagnaPrice);
            PremiumPrice.Text = String.Format("${0:f2}", myTransaction.PremiumPrice);

            TotalMagnaPrice.Text = String.Format("${0:f2}", myTransaction.MagnaPrice * myTransaction.OrderedMagna);
            TotalPremiumPrice.Text = String.Format("${0:f2}", myTransaction.PremiumPrice * myTransaction.OrderedPremium);

            TaxLbl.Text = String.Format("${0:f2}", myTransaction.Tax);
            DescuentoLbl.Text = String.Format("${0:f2}", myTransaction.Disccount);
            EnvioLbl.Text = String.Format("${0:f2}", myTransaction.DeliveryFee);
            SubtotalLbl.Text = String.Format("${0:f2}", myTransaction.Subtotal);
            TotalLbl.Text = String.Format("${0:f2}", myTransaction.Total);

			if (myTransaction.Status >= 1)
				StationChecker.Source = "FilledCircle.png";
            if (myTransaction.Status >= 2)
                OnTheWayChecker.Source = "FilledCircle.png";
            if (myTransaction.Status >= 3)
                FulfilledChecker.Source = "FilledCircle.png";
        }

        private async void ListOrdersBtn(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MainPage(1 /*User Id*/));
        }

    }
}