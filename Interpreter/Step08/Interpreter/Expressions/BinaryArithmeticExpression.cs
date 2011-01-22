namespace Interpreter.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualBasic.CompilerServices;

    public class BinaryArithmeticExpression : BinaryExpression
    {
        private ArithmeticOperator @operator;

        public BinaryArithmeticExpression(IExpression leftExpression, IExpression rightExpression, ArithmeticOperator @operator)
            : base(leftExpression, rightExpression)
        {
            this.@operator = @operator;
        }

        public ArithmeticOperator Operator { get { return this.@operator; } }

        public override object Apply(object leftValue, object rightValue)
        {
            switch (this.@operator)
            {
                case ArithmeticOperator.Add:
                    return Operators.AddObject(leftValue, rightValue);
                case ArithmeticOperator.Subtract:
                    return Operators.SubtractObject(leftValue, rightValue);
                case ArithmeticOperator.Multiply:
                    return Operators.MultiplyObject(leftValue, rightValue);
                case ArithmeticOperator.Divide:
                    return Operators.DivideObject(leftValue, rightValue);
            }

            throw new InvalidOperationException(string.Format("Unknow operator '{0}'", this.@operator));
        }
    }
}
