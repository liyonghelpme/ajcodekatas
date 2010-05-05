namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LocalObject : BaseObject
    {
        private IObject parent;

        public LocalObject(IObject parent)
        {
            this.parent = parent;
        }

        public IObject Parent { get { return this.parent; } }

        public override IObject Self
        {
            get
            {
                return this.parent.Self;
            }
        }

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

        public override void SetSlot(string name, object value)
        {
            this.parent.SetSlot(name, value);
        }

        public void SetLocalSlot(string name, object value)
        {
            this.slotValues[name] = value;
        }

        public override void UpdateSlot(string name, object value)
        {
            if (this.slotValues.ContainsKey(name))
            {
                this.slotValues[name] = value;
                return;
            }

            this.parent.UpdateSlot(name, value);
        }
    }
}
