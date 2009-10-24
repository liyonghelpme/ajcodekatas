namespace AjHask.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ComposeFunction : BaseFunction
    {
        private IFunction first;
        private IFunction second;

        public ComposeFunction(IFunction first, IFunction second)
        {
            this.first = first;
            this.second = second;
        }

        public override int Arity { get { return this.first.Arity; } }

        public override IFunction Apply(IFunction parameter)
        {
            IFunction result = this.first.Apply(parameter);

            if (result.Arity == 0)
                return this.second.Apply(result);

            return new ComposeFunction(result, this.second);
        }

        public override IFunction Bind(IList<IFunction> parameters)
        {
            IFunction newfirst = this.first.Bind(parameters);
            IFunction newsecond = this.second.Bind(parameters);

            if (newfirst.Equals(this.first) && newsecond.Equals(this.second))
                return this;

            return new ComposeFunction(newfirst, newsecond);
        }
    }
}
