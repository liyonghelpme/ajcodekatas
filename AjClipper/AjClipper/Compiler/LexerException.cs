namespace AjClipper.Compiler
{
    using System;

    public class LexerException : Exception
    {
        public LexerException(string message)
            : base(message)
        {
        }
    }
}
