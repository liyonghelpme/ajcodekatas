namespace AjAla.Compiler
{
    using System;

    internal class EndOfInputException : Exception
    {
        public EndOfInputException()
            : base("End of Input")
        {
        }
    }
}
