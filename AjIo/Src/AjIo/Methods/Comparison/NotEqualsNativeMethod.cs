namespace AjIo.Methods.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualBasic.CompilerServices;

    // TODO unify Native and not native implementation
    public class NotEqualsNativeMethod : ComparisonNativeMethod
    {
        public NotEqualsNativeMethod()
            : base(NotEquals)
        {
        }

        private static bool NotEquals(object obj1, object obj2)
        {
            return !Operators.Equals(obj1, obj2);
        }
    }
}
