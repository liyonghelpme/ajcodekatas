namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IntegerExpression : Expression
    {
        private int value;

        public IntegerExpression(int value)
        {
            this.value = value;
        }

        public int Value
        {
            get
            {
                return this.value;
            }
        }

        public override void Evaluate(Machine machine)
        {
            machine.Push(this.value);
        }
    }
}
