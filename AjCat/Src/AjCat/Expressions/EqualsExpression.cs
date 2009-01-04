namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EqualsExpression : Expression
    {
        private static EqualsExpression instance = new EqualsExpression();

        private EqualsExpression()
        {
        }

        public static EqualsExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            object value2 = machine.Pop();
            object value1 = machine.Pop();

            machine.Push(value1.Equals(value2));
        }

        public override string ToString()
        {
            return "eq";
        }
    }
}
