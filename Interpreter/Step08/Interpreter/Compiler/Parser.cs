namespace Interpreter.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Interpreter.Commands;
    using Interpreter.Expressions;

    public class Parser
    {
        private Lexer lexer;
        private Stack<Token> tokens = new Stack<Token>();

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
            IExpression expression = this.ParseFactorExpression();

            Token token = this.NextToken();

            while (token != null && token.TokenType == TokenType.Operator)
            {
                if (token.Value.Equals("+") || token.Value.Equals("-"))
                {
                    ArithmeticOperator oper = token.Value.Equals("+") ? ArithmeticOperator.Add : ArithmeticOperator.Subtract;
                    expression = new BinaryArithmeticExpression(expression, this.ParseFactorExpression(), oper);
                    token = this.NextToken();
                    continue;
                }

                break;
            }

            this.PushToken(token);

            return expression;
        }

        public ICommand ParseCommand()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Name && token.Value.Equals("if"))
                return this.ParseIfCommand();
            if (token.TokenType == TokenType.Name && token.Value.Equals("while"))
                return this.ParseWhileCommand();

            this.PushToken(token);

            return this.ParseSetCommand();
        }

        private ICommand ParseSetCommand()
        {
            string name = this.ParseName();
            this.ParseToken(TokenType.Operator, "=");
            IExpression expr = this.ParseExpression();
            this.ParseToken(TokenType.Separator, ";");

            return new SetCommand(name, expr);
        }

        private ICommand ParseIfCommand()
        {
            IExpression condition;
            ICommand thencmd;
            ICommand elsecmd;

            this.ParseToken(TokenType.Separator, "(");
            condition = this.ParseExpression();
            this.ParseToken(TokenType.Separator, ")");
            thencmd = this.ParseCommand();

            Token token = this.NextToken();

            if (token != null && token.TokenType == TokenType.Name && token.Value.Equals("else")) 
            {
                elsecmd = this.ParseCommand();
                return new IfCommand(condition, thencmd, elsecmd);
            }

            if (token != null)
                this.PushToken(token);

            return new IfCommand(condition, thencmd);
        }

        private ICommand ParseWhileCommand()
        {
            IExpression condition;
            ICommand cmd;

            this.ParseToken(TokenType.Separator, "(");
            condition = this.ParseExpression();
            this.ParseToken(TokenType.Separator, ")");
            cmd = this.ParseCommand();

            return new WhileCommand(condition, cmd);
        }

        private IExpression ParseFactorExpression()
        {
            IExpression expression = this.ParseSimpleExpression();

            Token token = this.NextToken();

            while (token != null && token.TokenType == TokenType.Operator)
            {
                if (token.Value.Equals("*") || token.Value.Equals("/"))
                {
                    ArithmeticOperator oper = token.Value.Equals("*") ? ArithmeticOperator.Multiply : ArithmeticOperator.Divide;
                    expression = new BinaryArithmeticExpression(expression, this.ParseSimpleExpression(), oper);
                    token = this.NextToken();
                    continue;
                }

                break;
            }

            this.PushToken(token);

            return expression;
        }

        private IExpression ParseSimpleExpression()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            if (token.TokenType == TokenType.Separator && token.Value.Equals("("))
            {
                IExpression expression = this.ParseExpression();
                token = this.NextToken();
                if (token == null || token.TokenType != TokenType.Separator || !token.Value.Equals(")"))
                    throw new ParserException("Expected ')'");
                return expression;
            }

            if (token.TokenType == TokenType.Integer)
                return new ConstantExpression(token.Value);

            if (token.TokenType == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.TokenType == TokenType.Name)
                return new VariableExpression((string)token.Value);

            throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));
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

        private void ParseToken(TokenType type, object value)
        {
            Token token = this.NextToken();

            if (token == null || token.TokenType != type || !value.Equals(token.Value))
                throw new ParserException(string.Format("Token '{0}' expected", value));
        }

        private string ParseName()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new ParserException("Name expected");

            if (token.TokenType != TokenType.Name)
                throw new ParserException(string.Format("Unexpected token '{0}'", token.Value));

            return (string)token.Value;
        }
    }
}
