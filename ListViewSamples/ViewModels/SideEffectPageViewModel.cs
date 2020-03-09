using System;
using System.Diagnostics;
using ListViewSamples.Models.SideEffect;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ListViewSamples.ViewModels
{
    /// <summary>
    /// 副作用画面ViewModel
    /// </summary>
    public class SideEffectPageViewModel : BindableBase, IInitialize, INavigatedAware
    {
        public Command TestCommand { get; }

        private ObservationRecord _record = new ObservationRecord { FollowUpName = "5分" };

        public SideEffectPageViewModel(INavigationService navi)
        {
            // 副作用項目マスタを作成する。
            ISideEffectItemMaster sideEffectMst = SideEffectItemMasterDummy.Create();

            // テストボタン。
            TestCommand = new Command( x =>
            {
                // 副作用入力画面を表示する。
                BasicSamplePageViewModel.Request.NavigateAsync(navi, sideEffectMst, _record);
            });
        }

        public void Initialize(INavigationParameters parameters)
        {
            Debug.WriteLine("SideEffectPageViewModel.Initialize()");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Debug.WriteLine("SideEffectPageViewModel.OnNavigatedFrom()");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Debug.WriteLine("SideEffectPageViewModel.OnNavigatedTo()");

            if ( parameters.GetNavigationMode() == NavigationMode.Back
                && BasicSamplePageViewModel.Result.GetParameter(parameters
                        , out BasicSamplePageViewModel.Result.Parameter result))
            {
                // 副作用入力画面からの戻り。
                _record = result.Record;
            }
        }
    }
}
