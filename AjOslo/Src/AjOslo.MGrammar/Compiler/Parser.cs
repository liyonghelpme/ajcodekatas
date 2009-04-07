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

        public TokenElement ParseToken()
        {
            ParseToken("token");

            string name = ParseName();

            TokenElement token = new TokenElement(name);

            ParseToken("=");

            for (Token tk = this.PeekToken(); tk != null && tk.Value != ";"; tk = this.PeekToken())
                switch (tk.TokenType)
                {
                    case TokenType.Name:
                        token.AddPrimaryExpression(this.ParseIdentifier());
                        break;
                    case TokenType.String:
                        token.AddPrimaryExpression(this.ParseTextLiteral());
                        break;
                    default:
                        throw new UnexpectedTokenException(tk);
                }

            ParseToken(";");

            return token;
        }

        public SyntaxElement ParseSyntax()
        {
            ParseToken("syntax");

            string name = ParseName();

            SyntaxElement syntax = new SyntaxElement(name);

            ParseToken("=");

            for (Token tk = this.PeekToken(); tk != null && tk.Value != ";"; tk = this.PeekToken())
                switch (tk.TokenType)
                {
                    case TokenType.Name:
                        syntax.AddPrimaryExpression(this.ParseIdentifier());
                        break;
                    case TokenType.String:
                        syntax.AddPrimaryExpression(this.ParseTextLiteral());
                        break;
                    default:
                        throw new UnexpectedTokenException(tk);
                }

            ParseToken(";");

            return syntax;
        }

        public Identifier ParseIdentifier()
        {
            string name = ParseName();

            return new Identifier(name);
        }

        private Token PeekToken()
        {
            Token token = lexer.NextToken();

            if (token == null)
                return null;

            lexer.PushToken(token);

            return token;
        }

        private string PeekTokenValue()
        {
            Token token = lexer.NextToken();

            if (token == null)
                return null;

            lexer.PushToken(token);

            return token.Value;
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

