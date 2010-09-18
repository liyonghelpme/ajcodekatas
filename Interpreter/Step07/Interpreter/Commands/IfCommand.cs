using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interpreter.Expressions;

namespace Interpreter.Commands
{
    public class IfCommand : ICommand
    {
        private IExpression condition;
        private ICommand thenCommand;
        private ICommand elseCommand;

        public IfCommand(IExpression condition, ICommand thenCommand)
            : this(condition, thenCommand, null)
        {
        }

        public IfCommand(IExpression condition, ICommand thenCommand, ICommand elseCommand)
        {
            this.condition = condition;
            this.thenCommand = thenCommand;
            this.elseCommand = elseCommand;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand ThenCommand { get { return this.thenCommand; } }

        public ICommand ElseCommand { get { return this.elseCommand; } }

        public void Execute(BindingEnvironment environment)
        {
            object result = this.condition.Evaluate(environment);
            bool cond = !IsFalse(result);

            if (cond)
                this.thenCommand.Execute(environment);
            else if (this.elseCommand != null)
                this.elseCommand.Execute(environment);
        }

        private static bool IsFalse(object obj)
        {
            if (obj == null)
                return true;

            if (obj is bool)
                return !(bool)obj;

            if (obj is int)
                return (int)obj == 0;

            if (obj is string)
                return string.IsNullOrEmpty((string)obj);

            if (obj is long)
                return (long)obj == 0;

            if (obj is short)
                return (short)obj == 0;

            if (obj is double)
                return (double)obj == 0;

            if (obj is float)
                return (float)obj == 0;

            return false;
        }
    }
}
