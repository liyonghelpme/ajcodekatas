namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjSoda;

    public class BaseBasicNewMethod : IMethod
    {
        public object Execute(object receiver, params object[] arguments)
        {
            IClass self = (IClass)receiver;

            return self.CreateInstance();
        }
    }
}
