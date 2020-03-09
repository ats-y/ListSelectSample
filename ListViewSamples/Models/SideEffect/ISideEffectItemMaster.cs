using System;
using System.Collections.Generic;

namespace ListViewSamples.Models.SideEffect
{
    /// <summary>
    /// 副作用マスタ。
    /// </summary>
    public interface ISideEffectItemMaster
    {
        /// <summary>
        /// 副作用なしが記録可能かどうか
        /// </summary>
        bool CanRecordNothing { get; }

        /// <summary>
        /// 副作用中止を記録可能かどうか
        /// </summary>
        bool CanRecordStop { get; }

        /// <summary>
        /// 副作用項目一覧。
        /// </summary>
        IReadOnlyCollection<SideEffectItem> Items { get; }
    }
}
