namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ElementComparer : IComparer<IElement>
    {
        public int Compare(IElement element1, IElement element2)
        {
            return element1.CompareTo(element2);
        }
    }
}
