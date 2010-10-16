using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Visitor;

namespace Patterns.Interpreter
{
    public class ArithmeticBinaryExpression : BinaryExpression
    {
        Func<object, object, object> operationFunction;
        ArithmeticOperation operation;

        public ArithmeticBinaryExpression(ArithmeticOperation oper, IExpression left, IExpression right)
            : base(left, right)        
        {
            this.operation = oper;

            switch (oper)
            {
                case ArithmeticOperation.Add:
                    this.operationFunction = (a, b) => (int) a + (int) b;
                    break;
                case ArithmeticOperation.Substract:
                    this.operationFunction = (a, b) => (int)a - (int)b;
                    break;
                case ArithmeticOperation.Multiply:
                    this.operationFunction = (a, b) => (int)a * (int)b;
                    break;
                case ArithmeticOperation.Divide:
                    this.operationFunction = (a, b) => (int)a / (int)b;
                    break;
            }
        }

        public ArithmeticOperation Operation { get { return this.operation; } }

        public override object Apply(object left, object right)
        {
            return this.operationFunction(left, right);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
