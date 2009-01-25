namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class BaseBehavior : BaseObject, IBehavior
    {
        public BaseBehavior()
        {
            this.Behavior = this;
            this.Methods = new Dictionary<string, IMethod>();
            this.Methods.Add("lookup:", new BaseLookupMethod());
            this.Methods.Add("methodAt:put:", new BaseAddMethodMethod());
            this.Send("methodAt:put:", "delegated", new BaseDelegateMethod());
            this.Send("methodAt:put:", "vtable", new BaseBehaviorMethod());
            this.Send("methodAt:put:", "allocate:", new BaseAllocateMethod());
        }

        public BaseBehavior(IObject behavior)
        {
            this.Behavior = behavior;
            this.Methods = new Dictionary<string, IMethod>();
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

        public virtual IMethod Lookup(string selector)
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

        public virtual void AddMethod(string selector, IMethod method)
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

        public virtual IBehavior CreateDelegated()
        {
            IBehavior delegated = new BaseBehavior(this.Behavior);
            delegated.Parent = this;

            return delegated;
        }

        public virtual IObject Allocate(int size)
        {
            if (size < 0)
            {
                throw new InvalidOperationException("Size is negative in allocate");
            }

            IObject obj = new BaseObject(size);
            obj.Behavior = this;

            return obj;
        }
    }
}
