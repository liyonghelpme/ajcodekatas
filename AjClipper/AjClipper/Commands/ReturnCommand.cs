namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class ReturnCommand : BaseCommand
    {
        private IExpression expression;

        public ReturnCommand(IExpression expression)
        {
            this.expression = expression;
        }

        public object Evaluate(ValueEnvironment environment)
        {
            if (this.expression == null)
                return null;

            return this.expression.Evaluate(environment);
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            throw new ReturnException(this.Evaluate(environment));
        }
    }

    public class ReturnException : Exception
    {
        private object value;

        public ReturnException()
        {
        }

        public ReturnException(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }
    }
}
