namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseGroup : IGroup
    {
        public abstract List<IElement> Elements { get; }

        public abstract int Order { get; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            
            if (!(obj is IGroup))
                return false;

            IGroup group = (IGroup)obj;

            if (this.Order != group.Order)
                return false;

            return GroupUtilities.AreEqual(this, group);
        }

        public override int GetHashCode()
        {
            int hash = 0;

            foreach (IElement element in this.Elements)
            {
                hash *= 5;
                hash += element.GetHashCode();
            }

            return hash;
        }
    }
}
