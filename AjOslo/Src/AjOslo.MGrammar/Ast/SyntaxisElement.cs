namespace AjOslo.MGrammar.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SyntaxElement : LanguageElement
    {
        private List<PrimaryExpression> expressions = new List<PrimaryExpression>();

        public SyntaxElement(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public ICollection<PrimaryExpression> Expressions
        {
            get
            {
                return this.expressions;
            }
        }

        public void AddPrimaryExpression(PrimaryExpression expression)
        {
            expressions.Add(expression);
        }
    }
}

