namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseAllocateMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IBehavior self = (IBehavior)receiver;
            int size = (int) arguments[0];

            return self.Allocate(size);
        }
    }
}
