namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsIntegerExpression : Expression
    {
        private static AsIntegerExpression instance = new AsIntegerExpression();

        private AsIntegerExpression()
        {
        }

        public static AsIntegerExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((int)machine.Pop());
        }

        public override string ToString()
        {
            return "as_int";
        }
    }
}
