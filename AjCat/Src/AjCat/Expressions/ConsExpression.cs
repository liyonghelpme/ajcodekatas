namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConsExpression : Expression
    {
        private static ConsExpression instance = new ConsExpression();

        private ConsExpression()
        {
        }

        public static ConsExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            object value = machine.Pop();
            IList list = (IList)machine.Pop();

            IList result = new ArrayList(list);
            result.Insert(0, value);

            machine.Push(result);
        }
    }
}
