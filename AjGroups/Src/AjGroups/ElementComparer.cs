namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ElementComparer : IComparer<Element>
    {
        public int Compare(Element element1, Element element2)
        {
            if (element1.Order < element2.Order)
            {
                return -1;
            }

            if (element1.Order > element2.Order)
            {
                return 1;
            }

            for (int k = 0; k < element1.Size; k++)
            {
                if (element1.Values[k] < element2.Values[k])
                {
                    return -1;
                }

                if (element1.Values[k] > element2.Values[k])
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
