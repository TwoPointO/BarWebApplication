using System;
using System.Collections.Generic;

namespace BarWebApplication.Models
{
    public partial class Category
    {
        public Category()
        {
            Item = new HashSet<Item>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
