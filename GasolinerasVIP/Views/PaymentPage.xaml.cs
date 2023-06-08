using GasolinerasVIP.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasolinerasVIP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        private int userId;
        private bool CardPaymentActive = false;
        Transaction myTransaction;
        Client client = new Client();
        public PaymentPage(Transaction transaction)
        {
            InitializeComponent();
            //userId = order.userId;
            userId = 1;
            myTransaction = transaction;
        }

        void CardPaymentClicked(object sender, EventArgs e)
        {
            // Change this to fetch form DB.
            if (!CardPaymentActive)
            {
                List<Payment> payments = new List<Payment> {
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

                ListView listView = new ListView
                {
                    ItemsSource = payments,
                    RowHeight = 90,
                    BackgroundColor = Color.FromHex("#F5F5F5"),
                    ItemTemplate = new DataTemplate(() =>
                    {
                        // Create views with bindings for displaying each property.
                        Label cardNickName = new Label();
                        cardNickName.SetBinding(Label.TextProperty, "CardNickName");

                        Label cardNumber = new Label();
                        cardNumber.SetBinding(Label.TextProperty, "CardNumber");

                        Label expDate = new Label();
                        expDate.SetBinding(Label.TextProperty,
                                new Binding("ShortED", BindingMode.OneWay,
                                    null, null, "Vencimiento: {0}"));

                        Image CardLogo = new Image { Source = "CreditCard.jpg", WidthRequest = 100 };

                        // Return an assembled ViewCell.
                        return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children = {
                                CardLogo,
                                new StackLayout {
                                    Orientation = StackOrientation.Vertical,
                                    Children =
                                    {
                                        cardNickName,
                                        cardNumber,
                                        expDate
                                    }
                                }
                                }
                            }
                        };
                    })
                };
                listView.ItemSelected += PaymentSelected;

                Button AddCardButton = new Button()
                {
                    Text = "Añadir Tarjeta",
                    FontSize = 20,
                    BackgroundColor = Color.FromHex("#F5F5F5"),
                    HeightRequest= 90,
                    ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Left, 5),
                    ImageSource = "AddCard.png",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    CornerRadius = 10
                };
                AddCardButton.Clicked += AddCardsClicked;

                CardStackLayout.Children.Add(listView);
                CardStackLayout.Children.Add(AddCardButton);
            } else
            {
                CardStackLayout.Children.RemoveAt(1);
                CardStackLayout.Children.RemoveAt(1);
            }
            CardPaymentActive = !CardPaymentActive;
        }
        private async void PaypalPaymentClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "Feature no implementada aun", "Volver");
        }
        private async void AddCardsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCardPage(userId));
        }
        private async void PaymentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await client.PostTransaction(myTransaction);
            await Navigation.PushAsync(new OrderPage(myTransaction));
        }
    }
}