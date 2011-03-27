namespace AjAla.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using AjAla.Language;
    using System.Globalization;
    using AjAla.Expressions;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public Parser(TextReader reader)
            : this(new Lexer(reader))
        {
        }

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public IExpression ParseExpression()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            switch (token.TokenType)
            {
                case TokenType.Integer:
                    return new ConstantExpression(Convert.ToInt32(token.Value, CultureInfo.InvariantCulture));
                case TokenType.String:
                    return new ConstantExpression(token.Value);
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }
    }
}
