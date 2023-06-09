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
	public class MyOrder
	{
		public Uri ImageUrl { get; set; }
		public string Total { get; set; }
		public string StationName { get; set; }
		public string Date { get; set; }
        public Transaction order { get; set; }
	}
    public partial class OrdersListPage : ContentPage
	{
        public List<Transaction> orders = new List<Transaction>();
        public List<MyOrder> myOrders = new List<MyOrder>();
        public string userId;
        Client client = new Client();   
        public OrdersListPage ()
		{
			InitializeComponent ();
			userId= Client.GetCurrUserId().Result;
            fetchOrders();
            
        }
        async void fetchOrders()
        {
            orders = await client.GetTransactions();
            parseOrders();
            ReceiptsList.ItemsSource = orders;
        }

        void parseOrders()
        {
            foreach (var order in orders)
            {
                 myOrders.Add(new MyOrder()
                {
                    ImageUrl = order.GasStation.ImageUrl,
                    Total = String.Format("Total = {0:f2}", order.Total),
                    StationName = order.GasStation.Name,
                    Date = String.Format("{0:M}", order.ReceivedOrderDate),
                    order = order
                });
            }
        }

        private async void OrderSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var myorder = (Transaction)e.SelectedItem;
            await Navigation.PushAsync(new OrderPage(myorder));
        }
    }
}