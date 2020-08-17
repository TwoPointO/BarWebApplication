using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarWebApplication.Models
{
    public class ViewModel
    {
        public IEnumerable<ItemOrder> ItemOrders { get; set; }
        public IEnumerable<DailyDealOrder> DailyDealOrders { get; set; }

        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<DailyDeal> DailyDeals { get; set; }
    }
}
