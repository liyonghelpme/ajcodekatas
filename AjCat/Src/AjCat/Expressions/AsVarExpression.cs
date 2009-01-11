namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsVarExpression : Expression
    {
        private static AsVarExpression instance = new AsVarExpression();

        private AsVarExpression()
        {
        }

        public static AsVarExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((object)machine.Pop());
        }

        public override string ToString()
        {
            return "as_var";
        }
    }
}
