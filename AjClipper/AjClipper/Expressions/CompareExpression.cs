namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class CompareExpression : BinaryExpression
    {
        private CompareOperator oper;

        public CompareExpression(IExpression leftExpression, IExpression rightExpression, CompareOperator oper)
            : base(leftExpression, rightExpression)
        {
            this.oper = oper;
        }

        public CompareExpression(object leftValue, object rightValue, CompareOperator oper)
            : base(leftValue, rightValue)
        {
            this.oper = oper;
        }

        protected override object EvaluateValues(object leftValue, object rightValue)
        {
            int result = ((IComparable)leftValue).CompareTo(rightValue);

            switch (this.oper)
            {
                case CompareOperator.Equal:
                    return result == 0;
                case CompareOperator.NotEqual:
                    return result != 0;
                case CompareOperator.Less:
                    return result < 0;
                case CompareOperator.Greater:
                    return result > 0;
                case CompareOperator.LessEqual:
                    return result <= 0;
                case CompareOperator.GreaterEqual:
                    return result >= 0;
            }

            throw new InvalidOperationException("Invalid comparison");
        }
    }
}
