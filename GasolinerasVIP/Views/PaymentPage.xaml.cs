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
        UserInfo user;
        Client client = new Client();
        public PaymentPage(Transaction transaction)
        {
            InitializeComponent();
            userId = Client.GetCurrUserId().Result;
            myTransaction = transaction;
            user = Client.GetCurrUser().Result;
        }

        private async void CardPaymentClicked(object sender, EventArgs e)
        {
            PaymentSelected(sender, e);
        }
        private async void CashPaymentClicked(object sender, EventArgs e)
        {
            PaymentSelected(sender, e);
        }

        private async void NoAddressError(object sender, EventArgs e)
        {
            await DisplayAlert("Error", "No hay dirección registrada.", "Volver");
        }

        private async void PaymentSelected(object sender, EventArgs e)
        {
            myTransaction.ApplicationUserId = Guid.Parse(userId);
            if(user.Address == null)
            {
                NoAddressError(sender, e);
                return;
            }
            await client.PostTransaction(myTransaction);
            await Navigation.PushAsync(new OrderPage(myTransaction));
        }
    }
}