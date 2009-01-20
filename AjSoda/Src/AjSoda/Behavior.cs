namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Behavior : BaseObject, IBehavior
    {
        private IMethod lookupMethod;
        private IMethod addMethodMethod;

        public Behavior() :
            base(2)
        {
            this.SetValueAt(1, new Dictionary<string, IMethod>());
        }

        public Behavior(IBehavior behavior)
            : this()
        {
            this.Behavior = behavior;
        }

        public IMethod Lookup(string selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            if (this.lookupMethod != null)
            {
                return (IMethod) this.lookupMethod.Execute(this, new object[] { selector });
            }
            else
            {
                IMethod method = this.Behavior.Lookup("lookup:");
                return (IMethod) method.Execute(this, new object[] { selector });
            }
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

            if (this.addMethodMethod != null)
            {
                this.addMethodMethod.Execute(this, new object[] { selector, method });
            }
            else
            {
                IMethod addMethod = this.Behavior.Lookup("addMethod:at:");
                addMethod.Execute(this, new object[] { selector, method });
            }

            // Internal cache
            if (selector == "addMethod:at:")
            {
                IDictionary<string, IMethod> methods = (IDictionary<string, IMethod>) this.GetValueAt(1);
                if (methods.ContainsKey(selector))
                {
                    this.addMethodMethod = methods[selector];
                }
            }
            else if (selector == "lookup:")
            {
                IDictionary<string, IMethod> methods = (IDictionary<string, IMethod>)this.GetValueAt(1);
                if (methods.ContainsKey(selector))
                {
                    this.lookupMethod = methods[selector];
                }
            }
        }
    }
}

