using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Models
{
    public class Item
    {
        public int ItemId { get; set; } = 1;
        public string ItemName { get; set; }
        public int ItemQuantity { get; set; }
        public int price { get; set; }
        public int total { get; set; }
    }
}
