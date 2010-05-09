namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjIo.Language;
    using System.Collections;

    public class ForEachMethod : IMethod
    {
        public object Execute(IObject context, IObject receiver, IList<object> arguments)
        {
            Message index = (Message)arguments[0];
            Message variable = (Message)arguments[1];
            Message body = (Message)arguments[2];

            if (index.Arguments != null)
                throw new InvalidOperationException("Invalid first argument in foreach");

            if (variable.Arguments != null)
                throw new InvalidOperationException("Invalid second argument in foreach");

            IEnumerable list = (IEnumerable)receiver;
            int n = 0;

            foreach (object obj in list)
            {
                LocalObject local = new LocalObject(context);
                local.SetLocalSlot(index.Symbol, n);
                local.SetLocalSlot(variable.Symbol, obj);

                body.Send(local, local);
                n++;
            }

            return receiver;
        }
    }
}
