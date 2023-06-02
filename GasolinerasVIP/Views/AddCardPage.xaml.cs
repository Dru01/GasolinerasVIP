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
	public partial class AddCardPage : ContentPage
	{
		private int userId;
		public AddCardPage (int UserId)
		{
			InitializeComponent ();
            userId = UserId;
        }
        private async void AddCardClicked(object sender, EventArgs e)
        {
            DisplayAlert("Tarjeta agregada", "Tu tarjeta ha sido agregada", "Ok");
            await Navigation.PushAsync(new MainPage(userId));
        }
    }
}