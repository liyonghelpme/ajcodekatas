namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GeneratedGroup : BaseGroup
    {
        private List<IElement> generators = new List<IElement>();
        private CompleteGroup internalGroup;

        public GeneratedGroup(params IElement[] generators)
        {
            foreach (Element generator in generators)
            {
                if (!this.generators.Contains(generator))
                {
                    this.generators.Add(generator);
                }
            }

            List<IElement> elements = this.GenerateElements();

            this.internalGroup = new CompleteGroup(elements);
        }

        public override int Order
        {
            get
            {
                return this.internalGroup.Order;
            }
        }

        public override List<IElement> Elements
        {
            get
            {
                return this.internalGroup.Elements;
            }
        }

        private List<IElement> GenerateElements()
        {
            return ElementUtilities.ElementsClosure(this.generators);
        }
    }
}
