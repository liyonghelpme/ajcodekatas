using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMapping.Domain
{
    public class Customer
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Notes { get; set; }
    }
}
