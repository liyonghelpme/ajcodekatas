namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class QuoteExpression : Expression
    {
        private static QuoteExpression instance = new QuoteExpression();

        private QuoteExpression()
        {
        }

        public static QuoteExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            object value = machine.Pop();
            machine.Push(new ConstantExpression(value));
        }
    }
}
