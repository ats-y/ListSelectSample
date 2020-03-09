using System;
using System.Collections.Generic;

namespace ListViewSamples.Models.SideEffect
{
    public class SideEffectItemMasterDummy : ISideEffectItemMaster
    {
        public SideEffectItemMasterDummy()
        {
        }

        public bool CanRecordNothing { get; private set; }

        public bool CanRecordStop { get; private set; }

        public IReadOnlyCollection<SideEffectItem> Items { get; private set; }

        /// <summary>
        /// 副作用項目一覧。
        /// </summary>
        private List<SideEffectItem> _items = new List<SideEffectItem>();

        public static SideEffectItemMasterDummy Create()
        {
            List<string> names = new List<string>()
            {
                "発熱",
                "悪寒・戦りつ",
                "熱感・ほてり",
                "そうよう感・かゆみ",
                "発赤・顔面紅潮",
                "呼吸困難（アノーゼ、喘鳴、呼吸状態悪化等） ",
                "嘔気・嘔吐",
                "嘔気・嘔吐",
                "血圧低下（収縮期血圧≧30mmHg の低下）",
                "血圧上昇（収縮期血圧≧30mmHg の上昇） ",
                "動悸・頻脈（成人：100 回／分以上、小児は年齢による頻脈の定義に従う）（その他長い説明長い説明長い説明長い説明長い説明長い説明長い説明）",
                "血管痛",
                "意識障害 ",
                "赤褐色尿（血色素尿）",
                "その他",
            };

            List<SideEffectItem> items = new List<SideEffectItem>();
            int codeIdx = 0;
            foreach (string name in names)
            {
                items.Add(
                    new SideEffectItem(name, $"I{codeIdx++.ToString("0000")}"));
            }

            return new SideEffectItemMasterDummy
            {
                Items = new List<SideEffectItem>(items),
                CanRecordNothing = true,
                CanRecordStop = true,
            };
        }
    }
}
