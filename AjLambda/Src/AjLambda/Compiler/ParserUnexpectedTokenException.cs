namespace AjLambda.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class ParserUnexpectedTokenException : ParserException
    {
        public ParserUnexpectedTokenException(string value)
            : base(string.Format(CultureInfo.CurrentCulture, "Unexpected {0}",value))
        {
        }
    }
}
