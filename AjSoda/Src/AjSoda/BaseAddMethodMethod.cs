namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseAddMethodMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IObject self = (IObject)receiver;
            string selector = (string) arguments[0];
            IMethod method = (IMethod) arguments[1];

            if (selector == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (method == null)
            {
                throw new ArgumentNullException("arguments");
            }

            IDictionary<string, IMethod> methods = (IDictionary<string, IMethod>) self.GetValueAt(1);

            if (methods == null)
            {
                methods = new Dictionary<string, IMethod>();
                self.SetValueAt(1, methods);
            }

            methods[selector] = method;

            return null;
        }
    }
}
