namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjSoda;

    public class BaseAddVariableMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IClass self = (IClass)receiver;
            string name = (string)arguments[0];

            self.AddVariable(name);

            // TODO review what to return
            return name;
        }
    }
}
