namespace AjIo.Methods.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class EqualsMethod : ComparisonMethod
    {
        public EqualsMethod()
            : base(Operators.Equals)
        {
        }
    }
}
