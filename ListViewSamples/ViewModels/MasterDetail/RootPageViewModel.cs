using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Navigation;

namespace ListViewSamples.ViewModels.MasterDetail
{
    public class RootPageViewModel : BindableBase
    {
        public ObservableCollection<MenuItem> Menus { get; } = new ObservableCollection<MenuItem>
        {
            new MenuItem
            {
                Title = "メインページ",
                PageName = "MainPage",
            },
            new MenuItem
            {
                Title = "ListViewサンプル",
                PageName = "BasicSamplePage",
            },
        };

        private INavigationService _naviService;

        private bool isPresented;

        public bool IsPresented
        {
            get { return isPresented; }
            set { SetProperty(ref this.isPresented, value); }
        }

        public RootPageViewModel(INavigationService naviService)
        {
            _naviService = naviService;
        }

        internal async Task PageChangeAsync(MenuItem menuItem)
        {
            await _naviService.NavigateAsync($"NavigationPage/{menuItem.PageName}");
            IsPresented = false;
        }
    }
}
