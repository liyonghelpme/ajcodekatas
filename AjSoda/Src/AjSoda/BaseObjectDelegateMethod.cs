namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseObjectDelegateMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IObject self = (IObject)receiver;
            IBehavior behavior = (IBehavior)self.Behavior;

            return behavior.CreateDelegated().Allocate(self.Size);
        }
    }
}
