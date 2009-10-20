namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PartialFunction : IFunction
    {
        private IFunction function;
        private List<IFunction> parameters = new List<IFunction>();

        public PartialFunction(IFunction function)
        {
            this.function = function;
        }

        public int Arity
        {
            get { return this.function.Arity - parameters.Count; }
        }

        public object Value { get { return this; } }

        public IFunction Apply(IFunction parameter)
        {
            parameters.Add(parameter);

            if (this.Arity == 0)
                return this.function.Apply(this.parameters);

            return this;
        }

        public IFunction Apply(IList<IFunction> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
