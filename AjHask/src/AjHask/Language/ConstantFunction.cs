namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConstantFunction : BaseFunction
    {
        private object value;

        public ConstantFunction(object value)
        {
            this.value = value;
        }

        public override int Arity { get { return 0; } }

        public override object Value { get { return this.value; } }

        public override IFunction Apply(IFunction parameter)
        {
            throw new InvalidOperationException();
        }
    }
}
