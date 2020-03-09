using System;
using System.Collections.Generic;

namespace ListViewSamples.Models.SideEffect
{
    /// <summary>
    /// 副作用経過観察のあるタイミングにおける副作用観察記録。
    /// </summary>
    public class ObservationRecord
    {
        /// <summary>
        /// 経過観察タイミング名（5分、15分、終了など）
        /// </summary>
        public string FollowUpName { get; set; }

        /// <summary>
        /// 選択副作用。
        /// </summary>
        public List<SideEffectItem> SelectedItems = new List<SideEffectItem>();

        /// <summary>
        /// 副作用なしかどうか。
        /// </summary>
        public bool Nothing { get; set; }

        /// <summary>
        /// 中止したかどうか。
        /// </summary>
        public bool Stop { get; set; }
        
        public ObservationRecord()
        {
        }
    }
}
