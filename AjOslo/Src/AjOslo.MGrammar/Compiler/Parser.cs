namespace AjOslo.MGrammar.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjOslo.MGrammar.Ast;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text) 
            : this(new Lexer(text))
        {
        }

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public TextLiteral ParseTextLiteral()
        {
            Token token = lexer.NextToken();

            return new TextLiteral(token.Value);
        }

        public ModuleNode ParseModule()
        {
            ParseToken("module");
            string name = ParseName();

            ModuleNode module = new ModuleNode(name);

            ParseToken("{");

            while (NextTokenIs("language"))
            {
                module.AddLanguage(this.ParseLanguage());
            }

            ParseToken("}");

            return module;
        }

        public LanguageNode ParseLanguage()
        {
            ParseToken("language");
            string name = ParseName();

            ParseToken("{");

            ParseToken("}");

            return new LanguageNode(name);
        }

        private string ParseName()
        {
            Token token = lexer.NextToken();

            if (token == null)
                throw new UnexpectedEndOfInputException();

            if (token.TokenType != TokenType.Name)
                throw new UnexpectedTokenException(token);

            return token.Value;
        }

        private void ParseToken(string value)
        {
            Token token = lexer.NextToken();

            if (token == null)
                throw new ExpectedTokenException(value);

            if (token.Value != value)
                throw new ExpectedTokenException(value);
        }

        private bool NextTokenIs(string value)
        {
            Token token = lexer.NextToken();

            if (token == null)
                return false;

            lexer.PushToken(token);

            return token.Value == value;
        }
    }
}

