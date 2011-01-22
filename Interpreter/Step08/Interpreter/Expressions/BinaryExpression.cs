namespace Interpreter.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BinaryExpression : IExpression
    {
        private IExpression leftExpression;
        private IExpression rightExpression;

        public BinaryExpression(IExpression leftExpression, IExpression rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }

        public IExpression LeftExpression { get { return this.leftExpression; } }

        public IExpression RightExpression { get { return this.rightExpression; } }

        public object Evaluate(BindingEnvironment environment)
        {
            object leftValue = this.leftExpression.Evaluate(environment);
            object rightValue = this.rightExpression.Evaluate(environment);

            return this.Apply(leftValue, rightValue);
        }

        public abstract object Apply(object leftValue, object rightValue);
    }
}
