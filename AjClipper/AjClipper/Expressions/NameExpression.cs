namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NameExpression : BaseExpression
    {
        private string name;

        public NameExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override object Evaluate(ValueEnvironment environment)
        {
            return environment.GetValue(this.name);
        }
    }
}
