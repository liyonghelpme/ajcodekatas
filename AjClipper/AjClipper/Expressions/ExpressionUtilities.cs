namespace AjClipper.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed class ExpressionUtilities
    {
        public static bool IsTrue(object value)
        {
            return !IsFalse(value);
        }

        public static bool IsFalse(object value)
        {
            if (value == null)
                return true;

            if (value is bool)
                return (bool)value;

            if (value is string)
                return string.IsNullOrEmpty((string)value);

            if (value is int)
                return ((int)value) == 0;

            if (value is short)
                return ((short)value) == 0;

            if (value is long)
                return ((long)value) == 0;

            if (value is decimal)
                return ((decimal)value) == 0;

            if (value is double)
                return ((double)value) == 0;

            if (value is float)
                return ((float)value) == 0;

            return false;
        }
    }
}
