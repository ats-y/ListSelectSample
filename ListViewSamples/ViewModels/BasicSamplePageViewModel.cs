using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using ListViewSamples.ViewModels.SideEffect;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Xamarin.Forms;

namespace ListViewSamples.ViewModels
{
    public class BasicSamplePageViewModel : BindableBase
    {
        public ObservableCollection<ItemViewModel> Items { get; } = new ObservableCollection<ItemViewModel>()
        {
            new ItemViewModel
            {
                Name = "発熱",
            },
            new ItemViewModel
            {
                Name = "悪寒・戦りつ",
            },
            new ItemViewModel
            {
                Name = "熱感・ほてり",
            },
            new ItemViewModel
            {
                Name = "そうよう感・かゆみ",
            },
            new ItemViewModel
            {
                Name = "発赤・顔面紅潮",
            },
            new ItemViewModel
            {
                Name = "呼吸困難（アノーゼ、喘鳴、呼吸状態悪化等） ",
            },
            new ItemViewModel
            {
                Name = "嘔気・嘔吐",
            },
            new ItemViewModel
            {
                Name = "嘔気・嘔吐",
            },
            new ItemViewModel
            {
                Name = "血圧低下（収縮期血圧≧30mmHg の低下）",
            },
            new ItemViewModel
            {
                Name = "血圧上昇（収縮期血圧≧30mmHg の上昇） ",
            },
            new ItemViewModel
            {
                Name = "動悸・頻脈（成人：100 回／分以上、小児は年齢による頻脈の定義に従う）（その他長い説明長い説明長い説明長い説明長い説明長い説明長い説明）",
            },
            new ItemViewModel
            {
                Name = "血管痛",
            },
            new ItemViewModel
            {
                Name = "意識障害 ",
            },
            new ItemViewModel
            {
                Name = "赤褐色尿（血色素尿）",
            },
            new ItemViewModel
            {
                Name = "その他",
            },
        };

        /// <summary>
        /// なしチェックボックスのチェック状態。
        /// </summary>
        public ReactiveProperty<bool> Nothing { get; } = new ReactiveProperty<bool>();

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
        public BasicSamplePageViewModel()
        {
            NothingSelectChangedCommand = new Command(OnNothingSelectChangedCommand);
            ItemSelectChangedCommand = new Command(OnItemSelectChangedCommand);
            RegistCommand = new Command(OnRegistCommand);
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
            Debug.WriteLine("OnNothingSelectChangedCommand(true)");
            foreach(ItemViewModel item in Items)
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
                Nothing.Value = false;
            };
        }

        /// <summary>
        /// 登録コマンドハンドラ。
        /// </summary>
        /// <param name="obj"></param>
        private void OnRegistCommand(object obj)
        {
        }
    }
}
