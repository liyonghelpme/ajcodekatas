namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AsListExpression : Expression
    {
        private static AsListExpression instance = new AsListExpression();

        private AsListExpression()
        {
        }

        public static AsListExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push((IList)machine.Pop());
        }

        public override string ToString()
        {
            return "as_list";
        }
    }
}
