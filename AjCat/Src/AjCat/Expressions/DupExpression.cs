namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DupExpression : Expression
    {
        private static DupExpression instance = new DupExpression();

        private DupExpression()
        {
        }

        public static DupExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push(machine.Top());
        }

        public override string ToString()
        {
            return "dup";
        }
    }
}
