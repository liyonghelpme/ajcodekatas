namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SymmetricGroup : GeneratedGroup
    {
        public SymmetricGroup(int size)
            : base(Element.CreateSwap(size), Element.CreateRotation(size))
        {
        }
    }
}
