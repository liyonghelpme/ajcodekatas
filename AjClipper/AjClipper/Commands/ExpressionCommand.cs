namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;
    using AjClipper.Language;

    public class ExpressionCommand : BaseCommand
    {
        private IExpression expression;

        public ExpressionCommand(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            if (this.expression != null)
                this.expression.Evaluate(environment);
        }
    }
}
