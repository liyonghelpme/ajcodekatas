namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EmptyExpression : Expression
    {
        private static EmptyExpression instance = new EmptyExpression();

        private EmptyExpression()
        {
        }

        public static EmptyExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            IList list = (IList)machine.Top();

            machine.Push(list.Count == 0);
        }

        public override string ToString()
        {
            return "empty";
        }
    }
}
