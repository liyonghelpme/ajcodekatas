namespace AjPepsi.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class EndOfInputException : Exception
    {
        public EndOfInputException()
            : base("End of Input")
        {
        }
    }
}
