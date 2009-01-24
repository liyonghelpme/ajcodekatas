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
            IBehavior self = (IBehavior)receiver;
            IMethod method = (IMethod) arguments[0];
            string selector = (string)arguments[1];

            self.AddMethod(selector, method);

            // TODO review this return
            return null;
        }
    }
}
