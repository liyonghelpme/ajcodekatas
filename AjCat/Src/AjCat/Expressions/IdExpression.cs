namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IdExpression : Expression
    {
        private static IdExpression instance = new IdExpression();

        private IdExpression()
        {
        }

        public static IdExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            // TODO improve
            machine.Push(machine.Pop());
        }

        public override string ToString()
        {
            return "id";
        }
    }
}
