namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ClonedObject : BaseObject
    {
        private IObject parent;

        public ClonedObject(IObject parent)
        {
            this.parent = parent;
        }

        public override IObject Parent { get { return this.parent; } }

        public override string TypeName
        {
            get { return this.parent.TypeName; }
        }

        public override object GetSlot(string name)
        {
            if (this.slotValues.ContainsKey(name))
                return this.slotValues[name];

            return this.parent.GetSlot(name);
        }
    }
}
