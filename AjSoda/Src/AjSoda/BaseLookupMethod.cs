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
            IBehavior self = (IBehavior)receiver;
            string selector = (string) arguments[0];

            return self.Lookup(selector);
        }
    }
}
