using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class SalesItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }

    }
}
