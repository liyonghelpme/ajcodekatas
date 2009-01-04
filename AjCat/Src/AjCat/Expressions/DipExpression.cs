namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DipExpression : Expression
    {
        private static DipExpression instance = new DipExpression();

        private DipExpression()
        {
        }

        public static DipExpression Instance
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
            object value = machine.Pop();

            expression.Evaluate(machine);

            machine.Push(value);
        }

        public override string ToString()
        {
            return "dip";
        }
    }
}
