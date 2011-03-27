namespace AjAla.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjAla.Language;

    public class SetValueCommand : ICommand
    {
        private string name;
        private IExpression expression;

        public SetValueCommand(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public void Execute(IContext context)
        {
            context.SetValue(this.name, this.expression.Evaluate(context));
        }
    }
}
