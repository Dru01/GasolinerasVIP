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
		public string ImageUrl { get; set; }
		public string Total { get; set; }
		public string StationName { get; set; }
		public string Date { get; set; }
        public Order order { get; set; }
	}
    public partial class OrdersListPage : ContentPage
	{
        public List<Order> orders = new List<Order>();
        public List<MyOrder> myOrders = new List<MyOrder>();
		public int userId;
        public OrdersListPage (int UserId)
		{
			InitializeComponent ();
			userId= UserId;
            fetchOrders();
            parseOrders();
            ReceiptsList.ItemsSource = myOrders;
        }

        // Change this to get it from the DB
        void fetchOrders()
        {
            GasStation gasStation1 = new GasStation()
            {
                Id = 0,
                ImageUrl = "https://lh3.googleusercontent.com/p/AF1QipNm5gQLj4HK63lxadUYf1adjysbjKdIZDNpNc10=s1360-w1360-h1020",
                Name = "Oxxo Gas",
                Location = new Tuple<double, double>(20.996465436160733, -101.28841488455917),
                PremiumPrice = 20.00,
                MagnaPrice = 17.00
            };
            GasStation gasStation2 = new GasStation()
            {
                Id = 1,
                ImageUrl = "https://lh3.googleusercontent.com/p/AF1QipO1EJEpwv5_hIUm-G5czSrJ9pp_FGyV4d75qgQI=s1360-w1360-h1020",
                Name = "Pemex",
                Location = new Tuple<double, double>(21.010613656306617, -101.27001362367888),
                PremiumPrice = 19.63,
                MagnaPrice = 17.42
            };
            orders = new List<Order>()
            {
                new Order()
                {
                    Id = 6,
                    userId = userId,
                    Station = gasStation1,
                    OrderedMagna = 4,
                    OrderedPremium = 2,
                    state =  3,
                },
                new Order()
                {
                    Id = 5,
                    userId = userId,
                    Station = gasStation2,
                    OrderedMagna = 18,
                    OrderedPremium = 0,
                    state =  1
                },
            };
        }



        void parseOrders()
        {
            foreach (var order in orders)
            {
                double total = order.OrderedMagna * order.Station.MagnaPrice + order.OrderedPremium * order.Station.PremiumPrice + 30.00;
                myOrders.Add(new MyOrder()
                {
                    ImageUrl = order.Station.ImageUrl,
                    Total = String.Format("Total = {0:f2}", total),
                    StationName = order.Station.Name,
                    Date = String.Format("{0:M}", order.TransactionTime),
                    order = order
                });
            }
        }

        private async void OrderSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var myorder = (MyOrder)e.SelectedItem;
            await Navigation.PushAsync(new OrderPage(myorder.order));
        }
    }
}