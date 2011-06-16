using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Tests.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
