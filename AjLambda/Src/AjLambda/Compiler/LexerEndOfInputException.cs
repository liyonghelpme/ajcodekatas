namespace AjLambda.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LexerEndOfInputException : LexerException
    {
        public LexerEndOfInputException()
            : base("Unexpected end of input")
        {
        }
    }
}
