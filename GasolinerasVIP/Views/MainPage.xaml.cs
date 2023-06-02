﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasolinerasVIP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage(int userId)
        {
            Children.Add(new NearPage());
            Children.Add(new OrdersListPage(userId));
            Children.Add(new MyUserPage(userId));
        }
    }
}