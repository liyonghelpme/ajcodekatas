using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;
using Patterns.Visitor;

namespace Patterns.Composite
{
    public class SetCommand : ICommand
    {
        private string name;
        private IExpression expression;

        public SetCommand(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public void Execute(IContext context)
        {
            object value = this.expression.Evaluate(context);
            context.SetValue(this.name, value);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
