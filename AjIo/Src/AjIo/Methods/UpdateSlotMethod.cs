namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class UpdateSlotMethod : BaseMethod
    {
        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            if (arguments == null || arguments.Count != 2)
                throw new InvalidOperationException("updateSlot should receive two arguments");

            string name = (string) arguments[0];
            receiver.UpdateSlot(name, arguments[1]);

            // TODO review return value
            return arguments[1];
        }
    }
}
