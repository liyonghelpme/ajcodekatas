namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsBoolExpression : Expression
    {
        private static AsBoolExpression instance = new AsBoolExpression();

        private AsBoolExpression()
        {
        }

        public static AsBoolExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((bool)machine.Pop());
        }

        public override string ToString()
        {
            return "as_bool";
        }
    }
}
