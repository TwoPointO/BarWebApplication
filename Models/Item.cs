using System;
using System.Collections.Generic;

namespace BarWebApplication.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemOrder = new HashSet<ItemOrder>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string ItemDescription { get; set; }
        public byte[] ImageData { get; set; }
        public int Category { get; set; }
        public bool Available { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<ItemOrder> ItemOrder { get; set; }
    }
}
