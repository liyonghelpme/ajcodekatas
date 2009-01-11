namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AExpression : Expression
    {
        private static AExpression instance = new AExpression();

        private AExpression()
        {
        }

        public static AExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            Expression expression = (Expression)machine.Pop();
            object value = machine.Pop();

            Machine newmachine = new Machine();
            newmachine.Push(value);

            expression.Evaluate(newmachine);

            machine.Push(newmachine.Pop());
        }

        public override string ToString()
        {
            return "A";
        }
    }
}
