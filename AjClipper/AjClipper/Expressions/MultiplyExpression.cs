namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class MultiplyExpression : BinaryExpression
    {
        public MultiplyExpression(IExpression leftExpression, IExpression rightExpression)
            : base(leftExpression, rightExpression)
        {
        }

        public MultiplyExpression(object leftValue, object rightValue)
            : base(leftValue, rightValue)
        {
        }

        protected override object EvaluateValues(object leftValue, object rightValue)
        {
            return Operators.MultiplyObject(leftValue, rightValue);
        }
    }
}
