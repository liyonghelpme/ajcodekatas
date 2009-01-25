namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseBehaviorMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            return ((IObject) receiver).Behavior;
        }
    }
}
