using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemsTablePerConcreteClass.Domain
{
    public class Note : Item
    {
        public virtual string Content { get; set; }
    }
}
