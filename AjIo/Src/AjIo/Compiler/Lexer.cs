namespace AjIo.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private TextReader reader;
        private Stack<int> chars = new Stack<int>();

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            this.SkipBlanks();

            int nxch = this.NextChar();

            if (nxch == -1)
                return null;

            char ch = (char)nxch;

            if (char.IsLetter(ch))
                return this.NextIdentifier(ch);

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (ch == ';' || ch == '\n')
                return new Token() { Value = ch.ToString(), TokenType = TokenType.Terminator };

            if (ch == '\r')
            {
                int nxch2 = this.NextChar();

                if (nxch2 != -1 && ((char) nxch2) == '\n')
                    return new Token() { Value = ch.ToString(), TokenType = TokenType.Terminator };

                this.Push(nxch2);
            }

            if (ch == '"')
                return this.NextString();

            if (ch == ',')
                return new Token() { Value = ",", TokenType = TokenType.Comma };

            if (ch == '(')
                return new Token() { Value = "(", TokenType = TokenType.LeftPar };

            if (ch == ')')
                return new Token() { Value = ")", TokenType = TokenType.RightPar };

            if (!char.IsControl(ch))
                return this.NextOperator(ch);

            throw new LexerException(string.Format("Unexpected character '{0}'", (char) ch));
        }

        private Token NextOperator(char ch)
        {
            string value = ch.ToString();

            int nxch = this.NextChar();

            while (nxch != -1)
            {
                char ch2 = (char) nxch;

                if (char.IsWhiteSpace(ch2))
                    break;

                if (char.IsLetterOrDigit(ch2)) 
                {
                    this.Push(nxch);
                    break;
                }

                value += ch2;

                nxch = this.NextChar();
            }

            return new Token() { Value = value, TokenType = TokenType.Operator };
        }

        private Token NextIdentifier(char ch)
        {
            string value = ch.ToString();

            int nxch = this.NextChar();

            while (nxch != -1)
            {
                char ch2 = (char) nxch;
                if (!char.IsLetterOrDigit(ch2) && ch2 != '.')
                {
                    this.Push(ch2);
                    break;
                }

                value += ch2;
                nxch = this.NextChar();
            }

            return new Token() { Value = value, TokenType = TokenType.Identifier };
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();

            int nxch = this.NextChar();

            while (nxch != -1)
            {
                char ch2 = (char)nxch;
                if (!char.IsDigit(ch2))
                {
                    this.Push(ch2);
                    break;
                }

                value += ch2;
                nxch = this.NextChar();
            }

            return new Token() { Value = value, TokenType = TokenType.Integer };
        }

        private Token NextString()
        {
            string value = string.Empty;

            int nxch = this.NextChar();

            while (nxch != -1)
            {
                char ch = (char) nxch;

                if (ch == '"')
                    break;

                if (ch == '\r' || ch == '\n') 
                    throw new LexerException("Not closed string");

                value += ch;

                nxch = this.NextChar();
            }

            if (nxch == -1)
                throw new LexerException("Not closed string");

            return new Token() { Value = value, TokenType = TokenType.String };
        }

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            return this.reader.Read();
        }

        private void SkipBlanks()
        {
            int ch = this.NextChar();

            while (ch != -1 && ((char)ch) != '\n' && ((char)ch) != '\r' && char.IsWhiteSpace((char)ch))
            {                
                ch = this.NextChar();
            }

            if (ch != -1)
                this.Push(ch);
        }

        private void Push(int ch)
        {
            this.chars.Push(ch);
        }
    }
}
