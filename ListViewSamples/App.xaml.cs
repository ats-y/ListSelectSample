using System;
using ListViewSamples.Models.SideEffect;
using ListViewSamples.ViewModels;
using ListViewSamples.ViewModels.MasterDetail;
using ListViewSamples.Views;
using ListViewSamples.Views.MasterDetail;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewSamples
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {

        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            // 起動直後にMainPageを表示する。
            NavigationService.NavigateAsync("RootPage/NavigationPage/SideEffectPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISideEffectItemMaster, SideEffectItemMasterDummy>();

            containerRegistry.RegisterForNavigation<NavigationPage>();

            // View「MainPage」ViewModels「MainPageViewModel」を登録する。
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RootPage, RootPageViewModel>();
            containerRegistry.RegisterForNavigation<BasicSamplePage, BasicSamplePageViewModel>();
            containerRegistry.RegisterForNavigation<SideEffectPage, SideEffectPageViewModel>();
        }
    }
}
