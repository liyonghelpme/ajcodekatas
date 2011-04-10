using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemsTablePerClass.Domain
{
    public class Item
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
    }
}
