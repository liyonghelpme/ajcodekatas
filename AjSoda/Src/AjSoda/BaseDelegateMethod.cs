namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseDelegateMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IBehavior self = (IBehavior)receiver;

            return self.CreateDelegated();
        }
    }
}
