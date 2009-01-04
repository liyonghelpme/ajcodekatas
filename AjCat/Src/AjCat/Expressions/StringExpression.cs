namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StringExpression : Expression
    {
        private string value;

        public StringExpression(string value)
        {
            this.value = value;
        }

        public string Value
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
            return string.Format("\"{0}\"", this.value.ToString());
        }
    }
}
