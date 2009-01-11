namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsCharExpression : Expression
    {
        private static AsCharExpression instance = new AsCharExpression();

        private AsCharExpression()
        {
        }

        public static AsCharExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((char)machine.Pop());
        }

        public override string ToString()
        {
            return "as_char";
        }
    }
}
