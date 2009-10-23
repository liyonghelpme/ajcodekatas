namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PartialFunction : IMultiFunction
    {
        private IMultiFunction function;
        private IFunction parameter;

        public PartialFunction(IMultiFunction function, IFunction parameter)
        {
            this.function = function;
            this.parameter = parameter;
        }

        public int Arity
        {
            get { return this.function.Arity - 1; }
        }

        public object Value { get { return this; } }

        public IFunction Apply(IFunction parameter)
        {
            if (this.function.Arity == 2)
            {
                IList<IFunction> parameters = new List<IFunction>();
                parameters.Add(this.parameter);
                parameters.Add(parameter);
                return this.function.Apply(parameters);
            }

            return new PartialFunction(this, parameter);
        }

        public IFunction Apply(IList<IFunction> parameters)
        {
            parameters.Insert(0, this.parameter);
            return this.function.Apply(parameters);
        }
    }
}
