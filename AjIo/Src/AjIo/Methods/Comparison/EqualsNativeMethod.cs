namespace AjIo.Methods.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class EqualsNativeMethod : ComparisonNativeMethod
    {
        public EqualsNativeMethod()
            : base(Operators.Equals)
        {
        }
    }
}
