using GasolinerasVIP.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GasolinerasVIP.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}