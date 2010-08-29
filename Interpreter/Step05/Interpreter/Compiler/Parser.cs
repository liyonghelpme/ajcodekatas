namespace Interpreter.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Interpreter.Expressions;

    public class Parser
    {
        private Lexer lexer;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public Parser(TextReader reader)
            : this(new Lexer(reader))
        {
        }

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public IExpression ParseExpression()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Integer)
                return new ConstantExpression(token.Value);

            if (token.TokenType == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.TokenType == TokenType.Name)
                return new VariableExpression((string) token.Value);

            throw new InvalidDataException(string.Format("Unexpected token '{0}'", token.Value));
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }
    }
}
