namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ComposeFunction : IFunction
    {
        private IFunction first;
        private IFunction second;

        public ComposeFunction(IFunction first, IFunction second)
        {
            this.first = first;
            this.second = second;
        }

        public int Arity { get { return this.first.Arity; } }

        public object Value { get { return this; } }

        public IFunction Apply(IFunction parameter)
        {
            return this.second.Apply(this.first.Apply(parameter));
        }
    }
}
