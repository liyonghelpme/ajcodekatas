using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;
using Patterns.Visitor;

namespace Patterns.Composite
{
    public class IfCommand : ICommand
    {
        private IExpression condition;
        private ICommand thencmd;
        private ICommand elsecmd;

        public IfCommand(IExpression condition, ICommand thencmd, ICommand elsecmd)
        {
            this.condition = condition;
            this.thencmd = thencmd;
            this.elsecmd = elsecmd;
        }

        public void Execute(IContext context)
        {
            object cond = this.condition.Evaluate(context);

            if (IsFalse(cond))
            {
                if (this.elsecmd != null)
                    this.elsecmd.Execute(context);
            }
            else
                this.thencmd.Execute(context);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        private bool IsFalse(object cond)
        {
            if (cond == null)
                return true;
            if (cond is Boolean)
                return !(bool)cond;

            if (cond is String)
                return String.IsNullOrEmpty((string)cond);

            return false;
        }
    }
}
