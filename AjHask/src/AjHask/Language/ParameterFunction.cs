namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ParameterFunction : BaseFunction
    {
        private int position;
        private int arity;

        public ParameterFunction(int position, int arity)
        {
            this.position = position;
            this.arity = arity;
        }

        public override int Arity { get { return this.arity; } }

        public int Position { get { return this.position; } }

        public override IFunction Apply(IFunction parameter)
        {
            return new CombineFunction(this, parameter);
        }

        public override IFunction Bind(IList<IFunction> parameters)
        {
            if (this.position < parameters.Count)
                return parameters[this.position];

            return new ParameterFunction(this.position - parameters.Count, this.arity);
        }
    }
}
