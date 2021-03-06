﻿namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SwapExpression : Expression
    {
        private static SwapExpression instance = new SwapExpression();

        private SwapExpression()
        {
        }

        public static SwapExpression Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Evaluate(Machine machine)
        {
            object top = machine.Pop();
            object subtop = machine.Pop();

            machine.Push(top);
            machine.Push(subtop);
        }

        public override string ToString()
        {
            return "swap";
        }
    }
}
