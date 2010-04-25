namespace AjIo.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Language;

    public class Parser
    {
        private Lexer lexer;
        private Stack<Token> tokens = new Stack<Token>();

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public object ParseExpression()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Identifier)
            {
                Token token2 = this.NextToken();

                if (token2 == null)
                    return new Message(token.Value);

                if (token2.TokenType == TokenType.LeftPar) 
                {
                    IList<object> arguments = this.ParseArguments();

                    return new Message(token.Value, arguments);
                }
            }

            if (token.TokenType == TokenType.Integer)
                return int.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture);

            if (token.TokenType == TokenType.String)
                return token.Value;

            throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));
        }

        private IList<object> ParseArguments()
        {
            IList<object> arguments = new List<object>();

            Token token = this.NextToken();

            while (token != null && token.TokenType != TokenType.RightPar)
            {
                if (arguments.Count > 0)
                {
                    if (token.TokenType != TokenType.Comma)
                        throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));
                }
                else
                    this.PushToken(token);

                arguments.Add(this.ParseExpression());
                token = this.NextToken();
            }

            if (token == null)
                throw new ParserException("Unexpected end of input");

            return arguments;
        }

        private Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            return this.lexer.NextToken();
        }

        private void PushToken(Token token)
        {
            this.tokens.Push(token);
        }
    }
}
