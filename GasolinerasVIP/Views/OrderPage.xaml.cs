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
		public OrderPage (Order myOrder)
		{
			InitializeComponent();
			DiaDeRegistro.Text = String.Format("Dia de registro: {0:M}", myOrder.TransactionTime);
			LugarDeEstacion.Text = String.Format("Lugar: {0}", myOrder.Station.Name);

			MagnaQtDtl.Text   = String.Format("{0}", myOrder.OrderedMagna);
			PremiumQtDtl.Text = String.Format("{0}", myOrder.OrderedPremium);

            MagnaPrice.Text   = String.Format("${0:f2}", myOrder.Station.MagnaPrice);
            PremiumPrice.Text = String.Format("${0:f2}", myOrder.Station.PremiumPrice);

			decimal tMagna   = myOrder.Station.MagnaPrice * myOrder.OrderedMagna;
			decimal tPremium = myOrder.Station.PremiumPrice * myOrder.OrderedPremium;

			TotalMagnaPrice.Text   = String.Format("${0:f2}", tMagna);
			TotalPremiumPrice.Text = String.Format("${0:f2}", tPremium);

			decimal total = tMagna + tPremium + 30.00m;
            TotalBill.Text = String.Format("${0:f2}", total);

			if (myOrder.state >= 1)
				StationChecker.Source = "FilledCircle.png";
            if (myOrder.state >= 2)
                OnTheWayChecker.Source = "FilledCircle.png";
            if (myOrder.state >= 3)
                FulfilledChecker.Source = "FilledCircle.png";
        }

        private async void ListOrdersBtn(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MainPage(1 /*User Id*/));
        }

    }
}