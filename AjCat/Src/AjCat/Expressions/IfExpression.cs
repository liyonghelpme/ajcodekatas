namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IfExpression : Expression
    {
        private static IfExpression instance = new IfExpression();

        private IfExpression()
        {
        }

        public static IfExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            Expression elseexpr = (Expression)machine.Pop();
            Expression thenexpr = (Expression)machine.Pop();
            bool condition = (bool)machine.Pop();

            if (condition) 
            {
                thenexpr.Evaluate(machine);
            }
            else
            {
                elseexpr.Evaluate(machine);
            }
        }
    }
}
