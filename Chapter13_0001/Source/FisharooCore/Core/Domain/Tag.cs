using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    public enum CloudSortOrder
    {
        Ascending,
        Descending,
        Random
    }

    public partial class Tag : IComparable<Tag> 
    {
        public decimal InitialValue { get; set; }
        public decimal MinimumOffset { get; set; }
        public decimal Ranged { get; set; }
        public decimal PreCalculatedValue { get; set; }
        public decimal FinalCalculatedValue { get; set; }
        public int FontSize { get; set; }

        public int CompareTo(Tag other)
        {
            return other.FinalCalculatedValue.CompareTo(this.FinalCalculatedValue);
        }
    }
}
