namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsStringExpression : Expression
    {
        private static AsStringExpression instance = new AsStringExpression();

        private AsStringExpression()
        {
        }

        public static AsStringExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((string)machine.Pop());
        }

        public override string ToString()
        {
            return "as_string";
        }
    }
}
