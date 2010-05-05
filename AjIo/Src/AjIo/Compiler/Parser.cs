namespace AjIo.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Language;

    public class Parser
    {
        private static string[] assigmentOperators = new string[] { "=", ":=", "::=" };
        private static Token terminator = new Token() { TokenType = TokenType.Terminator };

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

        public IMessage ParseExpression()
        {
            IMessage expression = this.ParseDelimitedMessage();

            if (expression == null)
                return null;

            Token token = this.NextToken();

            if (IsTerminator(token))
                return expression;

            throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));
        }

        private static bool IsAssigmentOperator(string oper)
        {
            return assigmentOperators.Contains(oper);
        }

        private static bool IsTerminator(Token token)
        {
            return token == null || token.TokenType == TokenType.Terminator;
        }

        private static bool IsCommaOrRightParenthesis(Token token)
        {
            return token != null && (token.TokenType == TokenType.Comma || token.TokenType == TokenType.RightPar);
        }

        private IMessage ParseMessageList()
        {
            IMessage message = this.ParseDelimitedMessage();

            if (message == null)
                return null;

            Token token = this.NextToken();

            if (!IsTerminator(token))
            {
                this.PushToken(token);
                return message;
            }

            MessageList messages = new MessageList(message);

            message = this.ParseDelimitedMessage();

            while (message != null)
            {
                messages.AddMessage(message);

                token = this.NextToken();

                if (!IsTerminator(token))
                {
                    this.PushToken(token);
                    break;
                }

                message = this.ParseDelimitedMessage();
            }

            return messages;
        }

        private IMessage ParseDelimitedMessage()
        {
            Token token = this.NextToken();

            while (token != null && token.TokenType == TokenType.Terminator)
                token = this.NextToken();

            if (token == null)
                return null;

            this.PushToken(token);

            if (IsCommaOrRightParenthesis(token))
                return null;

            IMessage msg = this.ParseSimpleMessage(true, true);

            token = this.NextToken();

            if (IsTerminator(token))
            {
                this.PushToken(token);
                return msg;
            }

            if (IsCommaOrRightParenthesis(token))
            {
                this.PushToken(token);
                return msg;
            }

            this.PushToken(token);

            MessageChain messages = new MessageChain(msg);

            messages.AddMessage(this.ParseSimpleMessage(true, false));

            token = this.NextToken();

            while (!IsTerminator(token))
            {
                this.PushToken(token);

                if (IsCommaOrRightParenthesis(token))
                    break;

                messages.AddMessage(this.ParseSimpleMessage(true, false));
                token = this.NextToken();
            }

            if (IsTerminator(token))
                this.PushToken(token);

            return messages;
        }

        private IMessage ParseAssigmentMessage(object left, string oper)
        {
            if (!(left is Message) || ((Message)left).Arguments != null)
                throw new ParserException("Invalid left value in assignment");

            left = ((Message)left).Symbol;

            object right = this.ParseDelimitedMessage();

            Message result = new Message(oper, new object[] { left, right });

            return result;
        }

        // TODO Refactor
        private IMessage ParseSimpleMessage(bool acceptOperator, bool acceptLeftParenthesis)
        {
            Token token = this.NextToken();

            if (token == null)
                throw new ParserException("Unexpected end of input");

            if (token.TokenType == TokenType.Identifier)
                return this.ParseSymbolMessage(token.Value);

            if (acceptOperator && token.TokenType == TokenType.Operator && !IsAssigmentOperator(token.Value))
                return this.ParseOperatorMessage(token.Value);

            if (acceptLeftParenthesis && token.TokenType == TokenType.LeftPar)
            {
                IMessage message = this.ParseDelimitedMessage();

                token = this.NextToken();

                if (token == null || token.TokenType != TokenType.RightPar)
                    throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));

                return message;
            }

            if (token.TokenType == TokenType.Integer)
                return new ObjectMessage(int.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture));

            if (token.TokenType == TokenType.String)
                return new ObjectMessage(token.Value);

            throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));
        }

        private IMessage ParseOperatorMessage(string oper)
        {
            return new Message(oper, new object[] { this.ParseSimpleMessage(false, true) });
        }

        private IMessage ParseSymbolMessage(string symbol)
        {
            Token token = this.NextToken();

            if (token == null)
                return new Message(symbol);

            if (token.TokenType == TokenType.LeftPar)
            {
                IList<object> arguments = this.ParseArguments();

                return new Message(symbol, arguments);
            }

            this.PushToken(token);

            Message msg = new Message(symbol);

            token = this.NextToken();

            if (token != null && token.TokenType == TokenType.Operator && IsAssigmentOperator(token.Value))
                return this.ParseAssigmentMessage(msg, token.Value);

            if (token != null)
                this.PushToken(token);

            return msg;
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

                arguments.Add(this.ParseMessageList());
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
