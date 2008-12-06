using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AjProlog.Core
{
    public class Parser
    {
        private TextReader input;
        private Token lastToken;
        private char lastChar;
        private bool hasChar;

        private const string separators = ",.()[]";

        public Parser(TextReader tr)
        {
            this.input = tr;
        }

        public Parser(string text) :
            this(new StringReader(text))
        {
        }

        private Token NextInteger(char ch)
        {
            string integer = ch.ToString();

            try
            {
                ch = NextChar();

                while (char.IsDigit(ch))
                {
                    integer += ch;
                    ch = NextChar();
                }

                PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Value = integer;
            token.Type = TokenType.Integer;

            return token;
        }

        private Token NextAtom(char ch)
        {
            string name = ch.ToString();

            try
            {
                ch = NextChar();

                while (char.IsLetterOrDigit(ch))
                {
                    name += ch;
                    ch = NextChar();
                }

                PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Value = name;
            token.Type = TokenType.Atom;

            return token;
        }

        private void PushChar(char ch)
        {
            this.lastChar = ch;
            this.hasChar = true;
        }

        public Token NextToken()
        {
            if (lastToken != null)
            {
                Token t = lastToken;
                lastToken = null;

                return t;
            }

            char ch;

            try
            {
                ch = NextCharSkipBlanks();

                if (char.IsDigit(ch))
                    return NextInteger(ch);

                if (char.IsLetter(ch))
                    return NextAtom(ch);

                if (IsSeparator(ch))
                    return NextSeparator(ch);

                return NextOperator(ch);
            }
            catch (EndOfInputException)
            {
                return null;
            }
        }

        private Token NextOperator(char ch)
        {
            string name = ch.ToString();

            try
            {
                ch = NextChar();

                while (!char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch) && !IsSeparator(ch))
                {
                    name += ch;
                    ch = NextChar();
                }

                PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token();
            token.Value = name;
            token.Type = TokenType.Atom;

            return token;
        }

        private Token NextSeparator(char ch)
        {
            string name = ch.ToString();

            Token token = new Token();
            token.Type = TokenType.Separator;
            token.Value = name;

            return token;
        }

        private bool IsSeparator(char ch)
        {
            return separators.IndexOf(ch) >= 0;
        }

        private char NextCharSkipBlanks()
        {
            char ch;

            ch = NextChar();

            while (char.IsWhiteSpace(ch))
                ch = NextChar();

            return ch;
        }

        private char NextChar()
        {
            if (hasChar)
            {
                hasChar = false;
                return lastChar;
            }

            int ch;

            if (input.Equals(System.Console.In) && input.Peek() <0)
            {
                Console.Out.Write("> ");
                Console.Out.Flush();
            }

            ch = input.Read();

            if (ch < 0)
                throw new EndOfInputException();

            return Convert.ToChar(ch);
        }
    }

    public enum TokenType
    {
        Integer,
        Atom,
        Separator
    }

    public class Token
    {
        public TokenType Type;
        public string Value;
    }

    public class EndOfInputException : Exception
    {
        public EndOfInputException()
            : base("End of Input")
        {
        }
    }
}
