namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseLookupMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IObject self = (IObject)receiver;

            if (receiver == null)
            {
                throw new ArgumentNullException("receiver");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (arguments.Length < 1)
            {
                throw new ArgumentNullException("arguments");
            }

            string selector = (string) arguments[0];

            if (receiver == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("arguments");
            }

            IDictionary<string, IMethod> methods = (IDictionary<string, IMethod>) self.GetValueAt(1);

            if (methods != null && methods.ContainsKey(selector))
            {
                return methods[selector];
            }

            IObject parent = (IObject)self.GetValueAt(0);

            if (parent != null)
            {
                return parent.Send("lookup:", selector);
            }

            return null;
        }
    }
}
