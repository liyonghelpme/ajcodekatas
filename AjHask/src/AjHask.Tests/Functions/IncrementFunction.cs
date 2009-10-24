namespace AjHask.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjHask.Language;

    class IncrementFunction : BaseFunction
    {
        public override IFunction Apply(IFunction parameter)
        {
            return new ConstantFunction(((int)(parameter.Value)) + 1);
        }

        public override int Arity
        {
            get { return 1; }
        }
    }
}
