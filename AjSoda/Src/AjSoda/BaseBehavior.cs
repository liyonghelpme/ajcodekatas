namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseBehavior : BaseObject, IBehavior
    {
        public BaseBehavior()
        {
            this.Behavior = this;
            this.Methods = new Dictionary<string, IMethod>();
            this.Methods.Add("lookup:", new BaseLookupMethod());
            this.Methods.Add("addMethod:at:", new BaseAddMethodMethod());
            this.Methods.Add("delegate", new BaseDelegateMethod());
        }

        public IDictionary<string, IMethod> Methods { get; set; }

        public IObject Parent { get; set; }

        public override object Send(string selector, params object[] arguments)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

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

            if (this.Parent != null)
            {
                return (IMethod) this.Parent.Send("lookup:", selector);
            }

            return null;
        }

        public void AddMethod(string selector, IMethod method)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            this.Methods[selector] = method;
        }

        public IBehavior CreateDelegated()
        {
            IBehavior delegated = new BaseBehavior();
            delegated.Behavior = this.Behavior;
            delegated.Parent = this;

            return delegated;
        }
    }
}
