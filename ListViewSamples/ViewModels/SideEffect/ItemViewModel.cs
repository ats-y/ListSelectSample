using System;
using ListViewSamples.Models.SideEffect;
using Reactive.Bindings;

namespace ListViewSamples.ViewModels.SideEffect
{
    /// <summary>
    /// 副作用項目ViewModel。
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// 副作用業務データ。
        /// </summary>
        public SideEffectItem Entity { get; internal set; }

        /// <summary>
        /// 副作用名。
        /// </summary>
        public string Name { get => Entity.Name; }

        /// <summary>
        /// 選択状態。
        /// </summary>
        public ReactiveProperty<bool> IsSelected { get; set; } = new ReactiveProperty<bool>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItemViewModel()
        {
        }
    }
}
