namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Language;

    public class FunctionMethod : BaseMethod
    {
        private Func<IObject, IObject, IList<object>, object> function;

        public FunctionMethod(Func<IObject, IObject, IList<object>, object> function)
        {
            this.function = function;
        }

        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            return this.function(context, receiver, arguments);
        }
    }
}
