namespace AjLambda.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Parser
    {
        private Lexer lexer;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public Parser(TextReader reader)
            : this(new Lexer(reader))
        {
        }

        public Expression ParseExpression()
        {
            Expression expression;
            Expression result = null;

            expression = this.ParseSimpleExpression();

            while (expression != null)
            {
                if (result == null)
                    result = expression;
                else
                    result = new Pair(result, expression);

                expression = this.ParseSimpleExpression();
            }

            return result;
        }

        private Expression ParseSimpleExpression()
        {
            Token token = NextToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Separator)
            {
                if (token.Value == "(")
                {
                    Expression expression = this.ParseExpression();
                    this.ParseToken(")");

                    return expression;
                }
            }
            else if (token.TokenType == TokenType.Operator)
            {
                if (token.Value == @"\")
                    return ParseLambda();
            }

            if (token.TokenType == TokenType.Variable)
                return new Variable(token.Value);

            this.PushToken(token);

            return null;
        }

        private Expression ParseLambda()
        {
            Variable parameter = this.ParseVariable();

            Stack<Variable> parameters = new Stack<Variable>();

            parameters.Push(parameter);

            Token token = this.NextToken();

            while (token != null && token.TokenType == TokenType.Variable)
            {
                parameters.Push(new Variable(token.Value));

                token = this.NextToken();
            }

            this.PushToken(token);

            this.ParseToken(".");

            Expression body = this.ParseExpression();

            while (parameters.Count>0)
            {
                parameter = parameters.Pop();
                body = new Lambda(parameter, body);
            }

            return body;
        }

        private Variable ParseVariable()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new LexerEndOfInputException();

            if (token.TokenType != TokenType.Variable)
                throw new ParserUnexpectedTokenException(token.Value);

            return new Variable(token.Value);
        }

        private void ParseToken(string value)
        {
            Token token = this.NextToken();

            if (token == null || token.Value != value)
                throw new ParserExpectedTokenException(value);
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }

        private void PushToken(Token token)
        {
            this.lexer.PushToken(token);
        }
    }
}

