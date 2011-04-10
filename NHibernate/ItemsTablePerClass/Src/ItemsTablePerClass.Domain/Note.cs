using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemsTablePerClass.Domain
{
    public class Note : Item
    {
        public virtual string Content { get; set; }
    }
}
