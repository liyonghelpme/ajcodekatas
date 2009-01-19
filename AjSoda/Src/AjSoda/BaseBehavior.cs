namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseBehavior : BaseObject, IBehavior
    {
        private Dictionary<string, IMethod> methods = new Dictionary<string, IMethod>();

        public IMethod Lookup(string selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("methodName");
            }

            if (this.methods.ContainsKey(selector))
            {
                return this.methods[selector];
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

            this.methods[selector] = method;
        }
    }
}
