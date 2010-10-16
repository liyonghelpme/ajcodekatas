using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Proxy
{
    public class Customer : ICustomer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Notes { get; set; }
    }
}
