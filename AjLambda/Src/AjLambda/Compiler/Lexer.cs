namespace AjLambda.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const string Operators = @"\<>=-+*/";
        private const string Separators = "().";
        private const char CommentCharacter = ';';

        private TextReader reader;
        private Stack<Token> tokenstack = new Stack<Token>();
        private bool hasChar;
        private char lastChar;

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public void PushToken(Token token)
        {
            this.tokenstack.Push(token);
        }

        public Token NextToken()
        {
            if (this.tokenstack.Count > 0)
            {
                return this.tokenstack.Pop();
            }

            char ch;

            try
            {
                ch = this.NextCharSkipBlanksAndComments();

                if (Char.IsLetter(ch))
                    if (Char.IsLower(ch))
                        return this.NextVariable(ch);
                    else
                        return this.NextName(ch);

                if (Char.IsDigit(ch))
                    return this.NextInteger(ch);

                if (Operators.IndexOf(ch) >= 0)
                    return this.NextOperator(ch);

                if (Separators.IndexOf(ch) >= 0)
                    return NextSeparator(ch);

                throw new LexerException("Invalid Character '" + ch + "'");
            }
            catch (LexerEndOfInputException)
            {
                return null;
            }
        }

        private static Token NextSeparator(char ch)
        {
            Token token = new Token();
            token.Value = new string(ch, 1);
            token.TokenType = TokenType.Separator;

            return token;
        }

        private Token NextInteger(char firstdigit)
        {
            string value = new string(firstdigit, 1);

            char ch;

            try
            {
                ch = this.NextChar();

                while (Char.IsDigit(ch))
                {
                    value += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (LexerEndOfInputException)
            {
            }

            Token token = new Token();
            token.TokenType = TokenType.Name;
            token.Value = value;

            return token;
        }

        private Token NextOperator(char firstchar)
        {
            string value = new string(firstchar, 1);

            char ch;

            try
            {
                ch = this.NextChar();

                while (Operators.IndexOf(ch) >= 0)
                {
                    value += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (LexerEndOfInputException)
            {
            }

            Token token = new Token();
            token.TokenType = TokenType.Operator;
            token.Value = value;

            return token;
        }

        private Token NextName(char firstchar)
        {
            StringBuilder sb = new StringBuilder(10);
            sb.Append(firstchar);

            try
            {
                char ch;

                ch = this.NextChar();

                while (Char.IsLetterOrDigit(ch))
                {
                    sb.Append(ch);
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (LexerEndOfInputException)
            {
            }

            Token token = new Token();
            token.TokenType = TokenType.Name;
            token.Value = sb.ToString();

            return token;
        }

        private void PushChar(char ch)
        {
            this.lastChar = ch;
            this.hasChar = true;
        }

        private char NextChar()
        {
            if (this.hasChar)
            {
                this.hasChar = false;
                return this.lastChar;
            }

            int ch = this.reader.Read();

            if (ch < 0)
                throw new LexerEndOfInputException();

            return (char)ch;
        }

        private Token NextVariable(char ch)
        {
            return new Token { TokenType = TokenType.Variable, Value = new string(ch, 1) };
        }

        private char NextCharSkipBlanks()
        {
            char ch;

            ch = this.NextChar();

            while (Char.IsWhiteSpace(ch))
                ch = this.NextChar();

            return ch;
        }

        private char NextCharSkipBlanksAndComments()
        {
            char ch;

            ch = this.NextCharSkipBlanks();

            // Skip Comments
            while (ch == CommentCharacter)
            {
                ch = this.NextChar();

                while (ch != '\n')
                    ch = this.NextChar();

                // After comment, skip blanks again
                ch = this.NextCharSkipBlanks();
            }

            return ch;
        }
    }
}
