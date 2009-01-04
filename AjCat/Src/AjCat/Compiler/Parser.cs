namespace AjCat.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Parser : IDisposable
    {
        private const string Separators = "[]{}";
        private const char StringDelimiter = '"';
        private const char StringEscapeChar = '\\';

        private static string[] otherOperators = new string[] { "**" };

        private TextReader reader;
        private Token lastToken;
        private char lastChar;
        private bool hasChar;

        public Parser(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.reader = new StringReader(text);
        }

        public Parser(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            this.reader = reader;
        }

        public Token NextToken()
        {
            if (this.lastToken != null)
            {
                Token t = this.lastToken;
                this.lastToken = null;

                return t;
            }

            char ch;

            try
            {
                ch = this.NextCharSkipBlanks();

                if (char.IsDigit(ch))
                {
                    return this.NextInteger(ch);
                }

                if (ch == StringDelimiter) 
                {
                    return this.NextString();
                }

                if (Separators.Contains(ch))
                {
                    return NextSeparator(ch);
                }

                return this.NextName(ch);

                throw new InvalidDataException("Unknown input");
            }
            catch (EndOfInputException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool dispose)
        {
            if (dispose && this.reader != null)
            {
                this.reader.Dispose();
            }
        }

        public void PushToken(Token token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            if (this.lastToken != null)
            {
                throw new InvalidOperationException();
            }

            this.lastToken = token;
        }

        private static char GetEscapedChar(char ch)
        {
            switch (ch)
            {
                case 't':
                    return '\t';
                case 'r':
                    return '\r';
                case 'n':
                    return '\n';
            }

            return ch;
        }

        private static Token NextSeparator(char ch)
        {
            return new Token()
            {
                TokenType = TokenType.Separator,
                Value = ch.ToString()
            };
        }

        private Token NextString()
        {
            StringBuilder sb = new StringBuilder();

            char ch = this.NextChar();

            while (ch != StringDelimiter)
            {
                if (ch == StringEscapeChar)
                {
                    ch = GetEscapedChar(this.NextChar());
                }

                sb.Append(ch);

                ch = this.NextChar();
            }

            Token token = new Token();
            token.TokenType = TokenType.String;
            token.Value = sb.ToString();

            return token;
        }

        private Token NextInteger(char ch)
        {
            string integer = ch.ToString();

            try
            {
                ch = this.NextChar();

                while (char.IsDigit(ch))
                {
                    integer += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Value = integer;
            token.TokenType = TokenType.Integer;

            return token;
        }

        private Token NextName(char ch)
        {
            string name = ch.ToString();

            try
            {
                ch = this.NextChar();

                while (!char.IsWhiteSpace(ch) && !Separators.Contains(ch))
                {
                    name += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Value = name;
            token.TokenType = TokenType.Name;

            return token;
        }

        private char NextCharSkipBlanks()
        {
            char ch;

            ch = this.NextChar();

            while (char.IsWhiteSpace(ch))
            {
                ch = this.NextChar();
            }

            return ch;
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

            int ch;

            if (this.reader.Equals(System.Console.In) && this.reader.Peek() < 0)
            {
                Console.Out.Write("> ");
                Console.Out.Flush();
            }

            ch = this.reader.Read();

            if (ch < 0)
            {
                throw new EndOfInputException();
            }

            return Convert.ToChar(ch);
        }
    }
}
