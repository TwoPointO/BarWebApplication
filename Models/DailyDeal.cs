using System;
using System.Collections.Generic;

namespace BarWebApplication.Models
{
    public partial class DailyDeal
    {
        public DailyDeal()
        {
            DailyDealOrder = new HashSet<DailyDealOrder>();
        }
        public int DailyDealId { get; set; }
        public string DailyDealName { get; set; }
        public byte[] DailyDealImage { get; set; }
        public string DailyDealDescription { get; set; }
        public decimal? DailyDealPrice { get; set; }
        public int? DailyDealQuantity { get; set; }
        public DateTime? DailyDealDate { get; set; }

        public virtual ICollection<DailyDealOrder> DailyDealOrder { get; set; }
    }
}
