namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ParameterFunction : IFunction
    {
        private int position;
        private int arity;

        public ParameterFunction(int position, int arity)
        {
            this.position = position;
            this.arity = arity;
        }

        public int Arity { get { return this.arity; } }

        public object Value { get { return this; } }

        public int Position { get { return this.position; } }

        public IFunction Apply(IFunction parameter)
        {
            return new CombineFunction(this, parameter);
        }

        public IFunction Bind(IList<IFunction> parameters)
        {
            if (this.position < parameters.Count)
                return parameters[this.position];

            return new ParameterFunction(this.position - parameters.Count, this.arity);
        }        
    }
}
