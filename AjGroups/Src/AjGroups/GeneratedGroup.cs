namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GeneratedGroup : CompleteGroup
    {
        public GeneratedGroup(params IElement[] generators)
            : base(ElementUtilities.ElementsClosure(generators))
        {
        }
    }
}
