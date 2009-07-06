namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BinaryExpression : BaseExpression
    {
        private IExpression leftExpression;
        private IExpression rightExpression;

        public BinaryExpression(IExpression leftExpression, IExpression rightExpression)
        {
            this.leftExpression = leftExpression;
            this.rightExpression = rightExpression;
        }

        public BinaryExpression(object leftValue, object rightValue)
            : this(new ConstantExpression(leftValue), new ConstantExpression(rightValue))
        {
        }

        public override object Evaluate(ValueEnvironment environment)
        {
            object leftValue = this.leftExpression.Evaluate(environment);
            object rightValue = this.rightExpression.Evaluate(environment);

            return this.EvaluateValues(leftValue, rightValue);
        }

        protected abstract object EvaluateValues(object leftValue, object rightValue);
    }
}
