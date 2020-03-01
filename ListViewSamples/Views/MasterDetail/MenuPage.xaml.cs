using System;
using System.Collections.Generic;
using ListViewSamples.ViewModels;
using ListViewSamples.ViewModels.MasterDetail;
using Xamarin.Forms;

namespace ListViewSamples.Views.MasterDetail
{
    public partial class MenuPage : ContentPage
    {
        private RootPageViewModel ViewModel => this.BindingContext as RootPageViewModel;

        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            await ViewModel.PageChangeAsync(args.SelectedItem as ViewModels.MasterDetail.MenuItem);
        }
    }
}
