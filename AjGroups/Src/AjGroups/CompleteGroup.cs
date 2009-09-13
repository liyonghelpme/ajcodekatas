namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompleteGroup : BaseGroup
    {
        private static ElementComparer comparer = new ElementComparer();
        private List<IElement> elements;

        public CompleteGroup(List<IElement> elements)
        {
            this.elements = new List<IElement>(elements);
            this.elements.Sort(comparer);
        }

        public override int Order
        {
            get
            {
                return this.elements.Count;
            }
        }

        public override List<IElement> Elements
        {
            get
            {
                return this.elements;
            }
        }
    }
}
