namespace AjCat.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FalseExpression : ConstantExpression
    {
        public FalseExpression()
            : base(false)
        {
        }
    }
}
