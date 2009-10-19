namespace AjHask.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjHask.Language;

    class IncrementFunction : IFunction
    {
        public object Apply(object parameter)
        {
            return ((int)parameter) + 1;
        }

        public object Apply(List<object> parameters)
        {
            throw new NotImplementedException();
        }

        public int Arity
        {
            get { return 1; }
        }
    }
}
