namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class NewSlotMethod : IMethod
    {
        public object Execute(IObject context, IObject receiver, IList<object> arguments)
        {
            if (arguments == null || arguments.Count != 2)
                throw new InvalidOperationException("newSlot should receive two arguments");

            string name = (string) arguments[0];
            receiver.SetSlot(name, arguments[1]);

            string setterName = "set" + char.ToUpper(name[0]) + name.Substring(1);

            receiver.SetSlot(setterName, new SetterMethod(name));

            // TODO review return value
            return arguments[1];
        }
    }
}
