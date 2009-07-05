namespace AjClipper.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Text;

    using AjClipper.Commands;
    using AjClipper.Expressions;

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

        public ICommand ParseCommand()
        {
            return ParseLineCommand();
        }

        private ICommand ParseLineCommand()
        {
            Token token = lexer.NextToken();

            if (token == null)
                return null;

            if (token.Value == "?")
                return new PrintLineCommand(this.ParseExpression());

            throw new ParserException(string.Format("Unknown command: {0}", token.Value));
        }

        private IExpression ParseExpression()
        {
            Token token = lexer.NextToken();

            if (token == null)
                return null;

            switch (token.TokenType)
            {
                case TokenType.String:
                    return new ConstantExpression(token.Value);
            }

            throw new ParserException(string.Format("Invalid expression: {0}", token.Value));
        }
    }
}
