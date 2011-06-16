using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel.Tests.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public DateTime Date { get; set; }

        public IList<OrderLine> OrderLines { get; set; }
    }
}
