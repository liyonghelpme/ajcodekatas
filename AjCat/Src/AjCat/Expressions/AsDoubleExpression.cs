namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsDoubleExpression : Expression
    {
        private static AsDoubleExpression instance = new AsDoubleExpression();

        private AsDoubleExpression()
        {
        }

        public static AsDoubleExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((double)machine.Pop());
        }

        public override string ToString()
        {
            return "as_dbl";
        }
    }
}
