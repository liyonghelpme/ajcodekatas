using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCat.Compiler
{
    public class UnexpectedTokenException : CompilerException
    {
        public UnexpectedTokenException(Token token)
            : base(string.Format("Unexpected '{0}'", token.Value))
        {
        }
    }
}
