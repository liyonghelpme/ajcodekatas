namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PartialFunction : BaseFunction
    {
        private IFunction function;
        private IList<IFunction> parameters;

        public PartialFunction(IFunction function, IList<IFunction> parameters)
        {
            this.function = function;
            this.parameters = parameters;
        }

        public PartialFunction(IFunction function, IFunction parameter)
        {
            this.function = function;
            this.parameters = new List<IFunction>();
            this.parameters.Add(parameter);
        }

        public override int Arity
        {
            get { return this.function.Arity - this.parameters.Count; }
        }

        public override IFunction Apply(IFunction parameter)
        {
            IList<IFunction> newparameters = new List<IFunction>(this.parameters);
            newparameters.Add(parameter);

            if (this.function.Arity <= newparameters.Count)
                return this.function.Evaluate(newparameters);

            return new PartialFunction(this.function, newparameters);
        }
    }
}
