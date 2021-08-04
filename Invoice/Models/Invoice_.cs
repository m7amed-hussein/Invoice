using Invoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice
{
    public class Invoice_
    {
        public static int Number = 1;
        public Invoice_()
        {
            Items = new List<Item>();
            client = new Client();
        }
        public Client client { get; set; }
        public int InvoideId { get; set; }
        public int All { get; set; }
        public List<Item> Items { get; set; }
    }


}
