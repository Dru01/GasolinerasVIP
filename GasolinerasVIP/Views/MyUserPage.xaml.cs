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
    public partial class MyUserPage : ContentPage
    {
        private List<Payment> payments = new List<Payment>();
        private int userId;
        public MyUserPage(int UserId)
        {
            userId= UserId;
            InitializeComponent();
            payments = new List<Payment> {
                new Payment() {
                    Id = 0,
                    UserId = userId,
                    CardNumber = 3884323224313183,
                    CardNickName = "Santander",
                    NameOnCard = "Pepe Pecas",
                    ExpirationDate = new DateTime(2024, 10, 09)
                },
                new Payment() {
                    Id = 1,
                    UserId = userId,
                    CardNumber = 7363323412831823,
                    CardNickName = "Bancomer Papa",
                    NameOnCard = "Jose Pecas",
                    ExpirationDate = new DateTime(2024, 2, 1)
                },
            };
            CardsList.ItemsSource = payments;

            Button AddCardButton = new Button()
            {
                Text = "Añadir Tarjeta",
                FontSize = 20,
                BackgroundColor = Color.FromHex("#F5F5F5"),
                HeightRequest = 90,
                ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 5),
                ImageSource = "AddCard.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                CornerRadius = 10
            };
            AddCardButton.Clicked += AddCardsClicked;
            MainStack.Children.Add(AddCardButton);
        }

        private async void AddCardsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCardPage(userId));
        }

        public void DeleteCard(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            
            DisplayAlert("Error", "Borrando Id: " + mi.CommandParameter, "Volver");
        }
    }
}