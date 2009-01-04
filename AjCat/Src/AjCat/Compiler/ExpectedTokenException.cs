namespace AjCat.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ExpectedTokenException : CompilerException
    {
        public ExpectedTokenException(string token)
            : base(string.Format("Expected '{0}'", token))
        {
        }
    }
}
