namespace AjIo.Methods.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    public class NotEqualsMethod : ComparisonMethod
    {
        public NotEqualsMethod()
            : base(NotEquals)
        {
        }

        private static bool NotEquals(object obj1, object obj2)
        {
            return !Operators.Equals(obj1, obj2);
        }
    }
}
