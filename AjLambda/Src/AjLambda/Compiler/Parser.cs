namespace AjLambda.Compiler
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Parser
    {
        private Lexer lexer;
        private Environment environment;

        public Parser(Lexer lexer, Environment environment)
        {
            this.lexer = lexer;
            this.environment = environment;
        }

        public Parser(Lexer lexer)
            : this(lexer, new Environment())
        {
        }

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public Parser(string text, Environment environment)
            : this(new Lexer(text), environment)
        {
        }

        public Parser(TextReader reader)
            : this(new Lexer(reader))
        {
        }

        public Parser(TextReader reader, Environment environment)
            : this(new Lexer(reader), environment)
        {
        }

        public Expression ParseExpression()
        {
            Expression expression;
            Expression result = null;

            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Name)
            {
                Token token2 = this.NextToken();

                if (token2 != null)
                {
                    if (token2.Value == "=")
                    {
                        expression = this.ParseExpression();
                        this.environment.DefineValue(token.Value, expression);
                        return expression;
                    }

                    this.PushToken(token2);
                }
            }

            this.PushToken(token);

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
            Token token = this.NextToken();

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
                    return this.ParseLambda();
            }

            if (token.TokenType == TokenType.Variable)
                return new Variable(token.Value);

            if (token.TokenType == TokenType.Name)
                return this.environment.GetValue(token.Value);

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

            while (parameters.Count > 0)
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

