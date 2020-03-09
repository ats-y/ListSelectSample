using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using ListViewSamples.Models.SideEffect;
using ListViewSamples.ViewModels.SideEffect;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Xamarin.Forms;

namespace ListViewSamples.ViewModels
{
    /// <summary>
    /// 副作用入力画面ViewModel
    /// </summary>
    public class BasicSamplePageViewModel : BindableBase, IInitialize
    {
        /// <summary>
        /// 副作用入力内容。
        /// </summary>
        private ObservationRecord _record;

        private INavigationService _navigationService;

        /// <summary>
        /// 選択肢となる副作用一覧。
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>();

        /// <summary>
        /// 中止チェックボックスの選択状態。
        /// </summary>
        public ReactiveProperty<bool> IsSelectedStop { get; } = new ReactiveProperty<bool>();

        /// <summary>
        /// 中止チェックボックスの表示。
        /// </summary>
        public ReactiveProperty<bool> IsVisibleStop { get; } = new ReactiveProperty<bool>();

        /// <summary>
        /// なしチェックボックスの表示。
        /// </summary>
        public ReactiveProperty<bool> IsVisibleNothing { get; } = new ReactiveProperty<bool>();

        /// <summary>
        /// なしチェックボックスの選択状態。
        /// </summary>
        public ReactiveProperty<bool> IsSelectedNothing { get; } = new ReactiveProperty<bool>();

        /// <summary>
        /// なしチェックボックスのチェック状態変更コマンド。
        /// </summary>
        public Command NothingSelectChangedCommand { get; }

        /// <summary>
        /// 副作用項目の選択状態変更コマンド。
        /// </summary>
        public Command ItemSelectChangedCommand { get; }

        /// <summary>
        /// 登録ボタンタップコマンド。
        /// </summary>
        public Command RegistCommand { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BasicSamplePageViewModel(INavigationService navi)
        {
            _navigationService = navi;
            NothingSelectChangedCommand = new Command(OnNothingSelectChangedCommand);
            ItemSelectChangedCommand = new Command(OnItemSelectChangedCommand);
            RegistCommand = new Command(OnRegistCommandAsync);
        }

        /// <summary>
        /// 画面初期化
        /// （旧NavigatingTo()）
        /// </summary>
        /// <param name="parameters"></param>
        public void Initialize(INavigationParameters parameters)
        {
            // パラメータを取得。
            Request.GetParameter(parameters
                , out ISideEffectItemMaster master
                , out _record);

            // 副作用マスタの内容に合わせて副作用一覧および「なし」「中止」の選択状況を設定する。
            foreach (SideEffectItem item in master.Items)
            {
                ItemViewModel vm = new ItemViewModel
                {
                    Entity = item,
                };
                vm.IsSelected.Value = _record.SelectedItems.Exists(x=>x==item);
                Items.Add(vm);
            }
            IsSelectedStop.Value = _record.Stop;
            IsSelectedNothing.Value = _record.Nothing;

            // マスタにあわせて「なし」「中止」を表示設定する。
            IsVisibleStop.Value = master.CanRecordStop;
            IsVisibleNothing.Value = master.CanRecordNothing;
        }

        /// <summary>
        /// なしチェックボックス変更コマンド。
        /// </summary>
        /// <param name="obj">なしチェックボックスの値</param>
        private void OnNothingSelectChangedCommand(object obj)
        {
            // なしチェックボックスが未チェックであれば何もしない。
            if (!(obj is bool isChecked) || !isChecked) return;

            // なしチェックボックスにチェックがついたら副作用項目を全部未選択にする。
            foreach (ItemViewModel item in Items)
            {
                item.IsSelected.Value = false;
            }
        }

        /// <summary>
        /// 副作用項目選択状態変更コマンドハンドラ。
        /// </summary>
        /// <param name="obj"></param>
        private void OnItemSelectChangedCommand(object obj)
        {
            // 副作用項目が一つでも選択状態であればなしチェックボックスを未チェックにする。
            if (Items.Any(x => x.IsSelected.Value))
            {
                IsSelectedNothing.Value = false;
            };
        }

        /// <summary>
        /// 登録コマンドハンドラ。
        /// </summary>
        /// <param name="obj"></param>
        private void OnRegistCommandAsync(object obj)
        {
            // 「なし」「中止」および副作用選択項目を遷移元の画面に渡す。
            _record.Stop = IsSelectedStop.Value;
            _record.Nothing = IsSelectedNothing.Value;
            _record.SelectedItems.Clear();
            _record.SelectedItems.AddRange(
                Items.Where(x => x.IsSelected.Value).Select(x => x.Entity));
            Result.GoBackAsync(_navigationService, _record);
        }

        /// <summary>
        /// 副作用入力画面表示要求。
        /// </summary>
        public static class Request
        {
            private const string KEY_MASTER = "MASTER";
            private const string KEY_RECORD = "RECORD";

            /// <summary>
            /// 副作用入力画面に遷移する。
            /// </summary>
            /// <param name="navi">ナビゲーションサービス</param>
            /// <param name="master">画面遷移パラメータ</param>
            /// <param name="record">副作用入力画面に表示する観察記録</param>
            public static async void NavigateAsync(INavigationService navi
                , ISideEffectItemMaster master, ObservationRecord record)
            {
                // パラメータチェック。
                if (master == null || record == null)
                {
                    throw new ArgumentException();
                }

                // 副作用入力画面に遷移する。
                NavigationParameters param = new NavigationParameters
                {
                    { KEY_MASTER, master },
                    { KEY_RECORD, record },
                };
                await navi.NavigateAsync("BasicSamplePage", param);
            }

            /// <summary>
            /// 画面遷移パラメータから副作用マスタと観察記録を取得する。
            /// </summary>
            /// <param name="parameters">画面遷移パラメータ</param>
            /// <param name="master">画面遷移パラメータ</param>
            /// <param name="record">作用入力画面に表示する観察記録</param>
            public static void GetParameter(INavigationParameters parameters,
                out ISideEffectItemMaster master, out ObservationRecord record)
            {
                master = parameters.GetValue<ISideEffectItemMaster>(KEY_MASTER);
                record = parameters.GetValue<ObservationRecord>(KEY_RECORD);
            }
        }

        /// <summary>
        /// 副作用入力画面応答。
        /// </summary>
        public class Result
        {
            private const string KEY_PARAM = "PARAM";

            /// <summary>
            /// 副作用入力画面を閉じ、表示元画面に遷移する。
            /// </summary>
            /// <param name="navi">ナビゲーションサービス</param>
            /// <param name="record">表示元画面に渡す観察記録</param>
            public static async void GoBackAsync(INavigationService navi,
                ObservationRecord record)
            {
                await navi.GoBackAsync(
                    new NavigationParameters
                    {
                        { KEY_PARAM, new Parameter{Record = record } },
                    });
            }

            /// <summary>
            /// ナビゲーションパラメータから副作用入力画面のパラメータを取得する。
            /// </summary>
            /// <param name="parameters">ナビゲーションパラメータ</param>
            /// <param name="p"></param>
            /// <returns></returns>
            public static bool GetParameter(INavigationParameters parameters, out Parameter p)
            {
                return parameters.TryGetValue(KEY_PARAM, out p);
            }

            /// <summary>
            /// 副作用入力画面から遷移元画面に渡すパラメータ。
            /// </summary>
            public class Parameter
            {
                /// <summary>
                /// 副作用入力画面で選択された観察記録。
                /// </summary>
                public ObservationRecord Record { get; set; }
            }
        }
    }
}
