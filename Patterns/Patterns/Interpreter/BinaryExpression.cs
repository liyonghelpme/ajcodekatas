using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Visitor;

namespace Patterns.Interpreter
{
    public abstract class BinaryExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public BinaryExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }
        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(IContext context)
        {
            object leftValue = this.left.Evaluate(context);
            object rightValue = this.right.Evaluate(context);

            return Apply(leftValue, rightValue);
        }

        public abstract object Apply(object left, object right);

        public abstract void Accept(IVisitor visitor);
    }
}
