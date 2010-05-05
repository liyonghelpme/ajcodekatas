namespace AjIo.Methods.Arithmetic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public abstract class BinaryArithmeticMethod : BaseNativeMethod
    {
        private Func<object, object, object> function;

        public BinaryArithmeticMethod(Func<object, object, object> function)
        {
            this.function = function;
        }

        public override object Apply(IObject context, object receiver, IList<object> arguments)
        {
            if (arguments == null || arguments.Count != 1)
                throw new InvalidOperationException("Binary Arithmetic Operator needs two arguments");

            object argument = arguments[0];

            return this.Apply(receiver, argument);
        }

        public virtual object Apply(object first, object second)
        {
            return this.function(first, second);
        }
    }
}
