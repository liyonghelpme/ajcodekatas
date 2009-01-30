namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GeneratedGroup : BaseGroup
    {
        private List<Element> generators = new List<Element>();
        private CompleteGroup internalGroup;

        public GeneratedGroup(params Element[] generators)
        {
            foreach (Element generator in generators)
            {
                if (!this.generators.Contains(generator))
                {
                    this.generators.Add(generator);
                }
            }

            List<Element> elements = this.GenerateElements();

            this.internalGroup = new CompleteGroup(elements);
        }

        public override int Order
        {
            get
            {
                return this.internalGroup.Order;
            }
        }

        public override List<Element> Elements
        {
            get
            {
                return this.internalGroup.Elements;
            }
        }

        private List<Element> GenerateElements()
        {
            return ElementUtilities.ElementsClosure(this.generators);
        }
    }
}
