namespace Interpreter.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Interpreter.Expressions;

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

        public void Execute(BindingEnvironment environment)
        {
            environment.SetValue(this.name, this.expression.Evaluate(environment));
        }
    }
}
