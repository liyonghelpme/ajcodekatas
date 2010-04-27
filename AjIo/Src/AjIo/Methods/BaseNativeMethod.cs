namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Language;

    public abstract class BaseNativeMethod : INativeMethod
    {
        public object Execute(IObject context, object receiver, IList<object> arguments)
        {
            return this.Apply(context, receiver, EvaluateArguments(context, arguments));            
        }

        public abstract object Apply(IObject context, object receiver, IList<object> arguments);

        // TODO this code is in BaseMethod, too
        private static IList<object> EvaluateArguments(IObject context, IList<object> arguments)
        {
            if (arguments == null)
                return null;

            IList<object> values = new List<object>();

            foreach (object obj in arguments)
                values.Add(context.Evaluate(obj));

            return values;
        }
    }
}
