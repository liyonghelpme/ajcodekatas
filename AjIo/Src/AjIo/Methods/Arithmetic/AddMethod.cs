namespace AjIo.Methods.Arithmetic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class AddMethod : BinaryArithmeticMethod
    {
        public AddMethod()
            : base(Operators.AddObject)
        {
        }
    }
}
