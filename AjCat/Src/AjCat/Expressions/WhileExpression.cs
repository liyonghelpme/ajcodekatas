namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class WhileExpression : Expression
    {
        private static WhileExpression instance = new WhileExpression();

        private WhileExpression()
        {
        }

        public static WhileExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            Expression condition = (Expression)machine.Pop();
            Expression block = (Expression)machine.Pop();

            condition.Evaluate(machine);

            while ((bool)machine.Pop())
            {
                block.Evaluate(machine);
                condition.Evaluate(machine);
            }
        }

        public override string ToString()
        {
            return "while";
        }
    }
}
