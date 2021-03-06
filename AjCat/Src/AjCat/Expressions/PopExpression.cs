﻿namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PopExpression : Expression
    {
        private static PopExpression instance = new PopExpression();

        private PopExpression()
        {
        }

        public static PopExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Pop();
        }

        public override string ToString()
        {
            return "pop";
        }
    }
}
