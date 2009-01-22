namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseBehavior : BaseObject
    {
        public BaseBehavior()
            : base(2)
        {
            this.Behavior = this;
            this.Methods = new Dictionary<string, IMethod>();
            this.Methods.Add("lookup:", new BaseLookupMethod());
            this.Methods.Add("addMethod:at:", new BaseAddMethodMethod());
        }

        public IDictionary<string, IMethod> Methods
        {
            get
            {
                return (IDictionary<string, IMethod>)this.GetValueAt(1);
            }

            private set
            {
                this.SetValueAt(1, value);
            }
        }

        public IObject Parent
        {
            get
            {
                return (IObject)this.GetValueAt(0);
            }

            set
            {
                this.SetValueAt(0, value);
            }
        }

        public override object Send(string selector, params object[] arguments)
        {
            if (selector == "lookup:")
            {
                return this.Lookup((string)arguments[0]);
            }

            return base.Send(selector, arguments);
        }

        public IMethod Lookup(string selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            if (this.Methods.ContainsKey(selector))
            {
                return this.Methods[selector];
            }

            return null;
        }
    }
}
