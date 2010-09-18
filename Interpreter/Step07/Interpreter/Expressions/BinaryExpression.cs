using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter.Expressions
{
    public abstract class BinaryExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

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

            return Apply(leftValue, rightValue);
        }

        public abstract object Apply(object leftValue, object rightValue);
    }
}
