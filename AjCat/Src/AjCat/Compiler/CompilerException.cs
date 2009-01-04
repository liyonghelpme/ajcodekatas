using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjCat.Compiler
{
    public abstract class CompilerException : Exception
    {
        protected CompilerException(string msg)
            : base(msg)
        {
        }
    }
}
