namespace AjLambda.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class ParserExpectedTokenException : ParserException
    {
        public ParserExpectedTokenException(string value)
            : base(string.Format(CultureInfo.CurrentCulture, "Expected {0}", value))
        {
        }
    }
}
