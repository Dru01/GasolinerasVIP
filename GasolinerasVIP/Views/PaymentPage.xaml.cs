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
        private string userId;
        private bool CardPaymentActive = false;
        Transaction myTransaction;
        Client client = new Client();
        public PaymentPage(Transaction transaction)
        {
            InitializeComponent();
            //userId = order.userId;
            userId = Client.GetCurrUserId().Result;
            myTransaction = transaction;
        }

        private async void CardPaymentClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "Feature no implementada aun", "Volver");
        }
        private async void PaypalPaymentClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "Feature no implementada aun", "Volver");
        }
        private async void PaymentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await client.PostTransaction(myTransaction);
            await Navigation.PushAsync(new OrderPage(myTransaction));
        }
    }
}