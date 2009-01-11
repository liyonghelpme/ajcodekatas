namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DoubleExpression : Expression
    {
        private double value;

        public DoubleExpression(double value)
        {
            this.value = value;
        }

        public double Value
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

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
