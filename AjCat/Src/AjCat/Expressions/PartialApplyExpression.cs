namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PartialApplyExpression : Expression
    {
        private static PartialApplyExpression instance = new PartialApplyExpression();

        private PartialApplyExpression()
        {
        }

        public static PartialApplyExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            CompositeExpression expression = (CompositeExpression) machine.Pop();
            object value = machine.Pop();

            List<Expression> newlist = new List<Expression>(expression.Expressions);

            newlist.Insert(0, new ConstantExpression(value));

            machine.Push(new CompositeExpression(newlist));
        }

        public override string ToString()
        {
            return "papply";
        }
    }
}
