namespace AjPepsi.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class TokenizerException : Exception
    {
        public TokenizerException(string msg)
            : base(msg)
        {
        }
    }
}
