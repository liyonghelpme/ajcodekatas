namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjIo.Language;

    public class IfMethod : IMethod
    {
        public object Execute(IObject context, IObject receiver, IList<object> arguments)
        {
            object condition = arguments[0];
            object result = context.Evaluate(condition);

            IMessage thenmsg = arguments.Count > 1 ? (IMessage) arguments[1] : null;
            IMessage elsemsg = arguments.Count > 2 ? (IMessage) arguments[2] : null;

            bool isFalse = result == null || (result is bool && !((bool)result));

            if (isFalse)
                if (elsemsg == null)
                    return result;
                else
                    return elsemsg.Send(context, context);

            if (thenmsg == null)
                return result;

            return thenmsg.Send(context, context);
        }
    }
}
