using System;
using Reactive.Bindings;

namespace ListViewSamples.ViewModels.SideEffect
{
    /// <summary>
    /// 副作用項目ViewModel。
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// 副作用名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 選択状態。
        /// </summary>
        public ReactiveProperty<bool> IsSelected { get; set; } = new ReactiveProperty<bool>();

        public ItemViewModel()
        {
        }
    }
}
