namespace AjOslo.MGrammar.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Identifier : PrimaryExpression
    {
        public Identifier(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
