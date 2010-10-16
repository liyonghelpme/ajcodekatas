using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Visitor;

namespace Patterns.Interpreter
{
    public class ConstantExpression : IExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }

        public object Evaluate(IContext context)
        {
            return this.value;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
