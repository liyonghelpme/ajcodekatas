using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public class ConstantExpression : IExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Evaluate()
        {
            return this.value;
        }
    }
}
