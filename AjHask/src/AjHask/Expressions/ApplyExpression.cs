namespace AjHask.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjHask.Language;

    public class ApplyExpression : IExpression
    {
        private IExpression functionExpression;
        private IExpression parameterExpression;

        public ApplyExpression(IExpression functionExpression, IExpression parameterExpression)
        {
            this.functionExpression = functionExpression;
            this.parameterExpression = parameterExpression;
        }

        public object Evaluate()
        {
            IFunction function = (IFunction) this.functionExpression.Evaluate();
            return function.Apply(this.parameterExpression.Evaluate());
        }
    }
}

