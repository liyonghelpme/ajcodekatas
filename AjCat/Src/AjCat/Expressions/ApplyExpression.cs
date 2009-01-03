namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ApplyExpression : Expression
    {
        private static ApplyExpression instance = new ApplyExpression();

        private ApplyExpression()
        {
        }

        public static ApplyExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            Expression expression = (Expression)machine.Pop();
            IList result = new ArrayList();

            expression.Evaluate(machine);
        }
    }
}
