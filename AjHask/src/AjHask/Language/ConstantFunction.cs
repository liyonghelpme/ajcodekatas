namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConstantFunction : IFunction
    {
        private object value;

        public ConstantFunction(object value)
        {
            this.value = value;
        }

        public int Arity { get { return 0; } }

        public object Value { get { return this.value; } }

        public IFunction Apply(IFunction parameter)
        {
            throw new NotSupportedException();
        }
    }
}
