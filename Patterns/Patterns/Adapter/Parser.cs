using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;
using Patterns.Composite;

namespace Patterns.Adapter
{
    public class Parser : IParser
    {
        private ILexer lexer;

        public Parser(ILexer lexer)
        {
            this.lexer = lexer;
        }

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public ICommand ParseCommand()
        {
            Token token = this.lexer.ReadToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Name)
            {
                string name = (string)token.Value;
                this.ParseToken(TokenType.Operator, "=");
                IExpression expression = this.ParseExpression();
                this.ParseToken(TokenType.Separator, ";");
                return new SetCommand(name, expression);
            }

            throw new NotImplementedException();
        }

        public IExpression ParseExpression()
        {
            Token token = this.lexer.ReadToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Integer)
                return new ConstantExpression(token.Value);

            if (token.TokenType == TokenType.Name)
                return new VariableExpression((string) token.Value);

            throw new InvalidProgramException();
        }

        private void ParseToken(TokenType type, object value)
        {
            Token token = this.lexer.ReadToken();

            if (token == null || token.TokenType != type || !token.Value.Equals(value))
                throw new InvalidProgramException(string.Format("Expected '{0}'", value));
        }
    }
}
