namespace AjCat.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NilExpression : Expression
    {
        private static NilExpression instance = new NilExpression();

        private NilExpression()
        {
        }

        public static NilExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push(new ArrayList());
        }
    }
}
