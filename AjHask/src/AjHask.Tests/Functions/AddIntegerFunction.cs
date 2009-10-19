namespace AjHask.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjHask.Language;

    class AddIntegerFunction : IFunction
    {
        public int Arity
        {
            get { return 2; }
        }

        public object Apply(object parameter)
        {
            PartialFunction function = new PartialFunction(this);
            return function.Apply(parameter);
        }

        public object Apply(List<object> parameters)
        {
            if (parameters == null || parameters.Count != this.Arity)
                throw new InvalidOperationException("Invalid number of parameters");

            return (int)parameters[0] + (int)parameters[1];
        }
    }
}
