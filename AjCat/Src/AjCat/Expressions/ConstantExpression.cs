namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConstantExpression : Expression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Value
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
