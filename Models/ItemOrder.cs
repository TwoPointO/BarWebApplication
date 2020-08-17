using System;
using System.Collections.Generic;
using BarWebApplication.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using BarWebApplication.Models;
using MySqlX.XDevAPI;

namespace BarWebApplication.Models
{
    public partial class ItemOrder
    {
        public int OrderId { get; set; }
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool OrderType { get; set; }
        public bool? CurrentStatus { get; set; }
        public string OrderMessage { get; set; }
        public int Table { get; set; }

        public string Message { get; set; }

        public virtual Item ItemNavigation { get; set; }
        public virtual OrderType OrderTypeNavigation { get; set; }        
    }
}
