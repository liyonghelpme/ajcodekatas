namespace AjClipper.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const string SingleCharOperators = "=$%+-*/:><#^!.";

        private const string SingleCharDelimiters = "[]{},()";

        private static string[] twoCharOperators = new string[] { ":=", "==", ">=", "<=", "<>", "!=", "->", "--", "++", "+=", "-=", "*=", "/=", "^=", "%=" };

        private TextReader reader;
        private Stack<char> stackedChars = new Stack<char>();
        private Stack<Token> stackedTokens = new Stack<Token>();

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
            this.stackedTokens.Push(token);
        }

        public Token NextToken()
        {
            if (this.stackedTokens.Count > 0)
                return this.stackedTokens.Pop();

            char ch;

            try
            {
                this.SkipBlanks();
                ch = this.NextChar();

                if (char.IsLetter(ch))
                    return this.NextName(ch);

                if (ch == '"')
                    return this.NextString();

                if (ch == '?')
                {
                    int ich = this.TryNextChar();

                    if (ich >= 0)
                    {
                        ch = (char) ich;

                        if (ch == '?')
                            return new Token() { Value = "??", TokenType = TokenType.Name };

                        this.PushChar(ch);
                    }

                    return new Token() { Value = "?", TokenType = TokenType.Name };
                }

                if (char.IsDigit(ch))
                    return this.NextInteger(ch);

                if (SingleCharDelimiters.IndexOf(ch) >= 0)
                    return new Token() { TokenType = TokenType.Delimiter, Value = new string(ch, 1) };

                if (SingleCharOperators.IndexOf(ch) >= 0)
                    return this.NextOperator(ch);
            }
            catch (EndOfInputException)
            {
                return null;
            }

            return null;
        }

        private Token NextName(char firstChar)
        {
            string name;

            name = new string(firstChar, 1);

            char ch;

            try
            {
                ch = this.NextChar();

                while (char.IsLetterOrDigit(ch))
                {
                    name += ch;
                    ch = this.NextChar();
                }

                if (ch == '.')
                {
                    name += ch;

                    return this.NextComplexName(name);
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token() { TokenType = TokenType.Name, Value = name.ToLower() };

            return token;
        }

        private Token NextComplexName(string name)
        {
            char ch;

            try
            {
                ch = this.NextChar();

                while (char.IsLetterOrDigit(ch) || ch=='.')
                {
                    name += ch;
                    ch = this.NextChar();
                }

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            Token token = new Token() { TokenType = TokenType.Name, Value = name };

            return token;
        }

        private Token NextOperator(char ch)
        {
            foreach (string oper in twoCharOperators) 
            {
                if (oper[0] == ch)
                {
                    int ich2 = this.TryNextChar();

                    if (ich2 >= 0)
                    {
                        char ch2 = (char) ich2;

                        if (oper[1] == ch2)
                        {
                            return new Token() { TokenType = TokenType.Operator, Value = oper };
                        }

                        this.PushChar(ch2);
                    }
                }
            }

            return new Token() { TokenType = TokenType.Operator, Value = new string(ch, 1) };
        }

        private Token NextInteger(char firstChar)
        {
            string integer;

            integer = new string(firstChar, 1);

            char ch;

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

            Token token = new Token() { TokenType = TokenType.Integer, Value = integer };

            return token;
        }

        private Token NextString()
        {
            string text = string.Empty;

            char ch;

            try
            {
                ch = this.NextChar();

                while (ch != '"')
                {
                    if (ch == '\\')
                        ch = this.NextChar();

                    text += ch;
                    ch = this.NextChar();
                }
            }
            catch (EndOfInputException)
            {
                throw new LexerException("Unclosed string");
            }

            Token token = new Token() { TokenType = TokenType.String, Value = text };

            return token;
        }

        private void PushChar(char ch)
        {
            this.stackedChars.Push(ch);
        }

        private char NextChar()
        {
            if (this.stackedChars.Count > 0)
            {
                return this.stackedChars.Pop();
            }

            int ch;
            ch = this.reader.Read();

            if (ch < 0)
            {
                throw new EndOfInputException();
            }

            return (char)ch;
        }

        private int TryNextChar()
        {
            if (this.stackedChars.Count > 0)
                return (int) this.stackedChars.Pop();

            int ch;
            ch = this.reader.Read();

            return ch;
        }

        private void SkipBlanks()
        {
            char ch;

            ch = this.NextChar();

            while (char.IsWhiteSpace(ch))
            {
                ch = this.NextChar();
            }

            this.PushChar(ch);
        }
    }
}
