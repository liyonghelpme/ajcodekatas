namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseAddMethodMethod : IMethod
    {
        public object Execute(object receiver, object[] arguments)
        {
            IObject self = (IObject)receiver;
            string selector = (string) arguments[0];
            IMethod method = (IMethod) arguments[1];

            IDictionary<string, IMethod> methods = (IDictionary<string, IMethod>) self.GetValueAt(1);

            methods[selector] = method;

            return null;
        }
    }
}
