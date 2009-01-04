namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ClearExpression : Expression
    {
        private static ClearExpression instance = new ClearExpression();

        private ClearExpression()
        {
        }

        public static ClearExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Clear();
        }

        public override string ToString()
        {
            return "#clr";
        }
    }
}
