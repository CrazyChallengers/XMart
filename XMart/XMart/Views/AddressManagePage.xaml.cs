﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XMart.ViewModels;

namespace XMart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressManagePage : ContentPage
    {
        AddressManageViewModel addressManageViewModel = new AddressManageViewModel();

        public AddressManagePage()
        {
            InitializeComponent();

            BindingContext = addressManageViewModel;
        }

        protected override void OnDisappearing()
        {
            list.SelectedItem = -1;
        }
    }
}