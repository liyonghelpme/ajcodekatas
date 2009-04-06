namespace AjOslo.MGrammar.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TextLiteral
    {
        public TextLiteral(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}
