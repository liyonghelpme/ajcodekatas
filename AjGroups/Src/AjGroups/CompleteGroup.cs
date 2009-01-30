namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompleteGroup : BaseGroup
    {
        private static ElementComparer comparer = new ElementComparer();
        private List<Element> elements;

        public CompleteGroup(List<Element> elements)
        {
            this.elements = new List<Element>(elements);
            this.elements.Sort(comparer);
        }

        public override int Order
        {
            get
            {
                return this.elements.Count;
            }
        }

        public override List<Element> Elements
        {
            get
            {
                return this.elements;
            }
        }
    }
}
