namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class SubtractExpression : BinaryExpression
    {
        public SubtractExpression(IExpression leftExpression, IExpression rightExpression)
            : base(leftExpression, rightExpression)
        {
        }

        public SubtractExpression(object leftValue, object rightValue)
            : base(leftValue, rightValue)
        {
        }

        protected override object EvaluateValues(object leftValue, object rightValue)
        {
            if (leftValue is string)
            {
                leftValue = ((string)leftValue).TrimEnd(' ');
                return Operators.ConcatenateObject(leftValue, rightValue);
            }

            return Operators.SubtractObject(leftValue, rightValue);
        }
    }
}
