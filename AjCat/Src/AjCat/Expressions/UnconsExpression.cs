namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UnconsExpression : Expression
    {
        private static UnconsExpression instance = new UnconsExpression();

        private UnconsExpression()
        {
        }

        public static UnconsExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            IList list = (IList)machine.Pop();

            IList result = new ArrayList(list);
            object value = result[0];
            result.RemoveAt(0);

            machine.Push(result);
            machine.Push(value);
        }

        public override string ToString()
        {
            return "uncons";
        }
    }
}
