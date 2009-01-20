namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseLookupMethod : IMethod
    {
        public object Execute(object receiver, object[] arguments)
        {
            IObject self = (IObject)receiver;
            string selector = (string) arguments[0];

            IDictionary<string, IMethod> methods = (IDictionary<string, IMethod>) self.GetValueAt(1);

            if (methods.ContainsKey(selector))
            {
                return methods[selector];
            }

            IBehavior parent = (IBehavior)self.GetValueAt(0);

            if (parent != null)
            {
                return parent.Lookup(selector);
            }

            return null;
        }
    }
}
