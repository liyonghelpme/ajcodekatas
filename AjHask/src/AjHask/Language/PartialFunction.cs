namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PartialFunction : IFunction
    {
        private IFunction function;
        private List<object> parameters = new List<object>();

        public PartialFunction(IFunction function)
        {
            this.function = function;
        }

        public int Arity
        {
            get { return this.function.Arity - parameters.Count; }
        }

        public object Apply(object parameter)
        {
            parameters.Add(parameter);

            if (this.Arity == 0)
                return this.function.Apply(this.parameters);

            return this;
        }

        public object Apply(List<object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
