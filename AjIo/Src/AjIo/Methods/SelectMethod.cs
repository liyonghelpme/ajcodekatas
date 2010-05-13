namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjIo.Language;
    using System.Collections;

    public class SelectMethod : IMethod
    {
        public object Execute(IObject context, IObject receiver, IList<object> arguments)
        {
            Message variable = (Message)arguments[0];
            IMessage body = (IMessage)arguments[1];

            if (variable.Arguments != null)
                throw new InvalidOperationException("Invalid first argument in foreach");

            IEnumerable list = (IEnumerable)receiver;
            List<object> selected = new List<object>();

            foreach (object obj in list)
            {
                LocalObject local = new LocalObject(context);
                local.SetLocalSlot(variable.Symbol, obj);

                object result = body.Send(local, local);

                // TODO IsFalse predicate to unify this code
                if (result == null || (result is bool && !((bool)result)))
                    continue;

                selected.Add(obj);
            }

            // TODO ListObject.Create(context,element)to unify this code
            IObject top = context;

            while (top.Parent != null)
                top = top.Parent;

            return new ListObject(top, selected);
        }
    }
}
