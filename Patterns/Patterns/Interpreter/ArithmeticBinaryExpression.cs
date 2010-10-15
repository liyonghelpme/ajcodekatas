using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Interpreter
{
    public class ArithmeticBinaryExpression : BinaryExpression
    {
        public ArithmeticBinaryExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object left, object right)
        {
            throw new NotImplementedException();
        }
    }
}
