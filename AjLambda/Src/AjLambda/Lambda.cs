namespace AjLambda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lambda : Expression
    {
        private Variable parameter;
        private Expression body;

        public Lambda(Variable parameter, Expression body)
        {
            this.parameter = parameter;
            this.body = body;
        }

        public override string ToString()
        {
            return "\\"+parameter.ToString()+"."+body.ToString();
        }
    }
}

