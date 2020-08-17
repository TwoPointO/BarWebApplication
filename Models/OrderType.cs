using System;
using System.Collections.Generic;

namespace BarWebApplication.Models
{
    public partial class OrderType
    {
        public OrderType()
        {
            ItemOrder = new HashSet<ItemOrder>();
            DailyDealOrder = new HashSet<DailyDealOrder>();
        }

        public bool TypeId { get; set; }
        public string OrderType1 { get; set; }

        public virtual ICollection<ItemOrder> ItemOrder { get; set; }
        public virtual ICollection<DailyDealOrder> DailyDealOrder { get; set; }
    }
}
