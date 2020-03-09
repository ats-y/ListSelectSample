using System;
namespace ListViewSamples.Models.SideEffect
{
    /// <summary>
    /// 副作用項目。
    /// </summary>
    public class SideEffectItem
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public SideEffectItem(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
