namespace AjIo.Methods.Arithmetic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class MultiplyMethod : BinaryArithmeticMethod
    {
        public MultiplyMethod()
            : base(Operators.MultiplyObject)
        {
        }
    }
}
