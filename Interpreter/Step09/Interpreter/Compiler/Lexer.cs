namespace Interpreter.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const char StringDelimeter = '"';
        private const char StringEscape = '\\';

        private static string[] separators = new string[] { ";", "(", ")", "{", "}" };
        private static string[] operators = new string[] { "=", "+", "-", "*", "/" };

        private TextReader reader;
        private Stack<int> chars = new Stack<int>();

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public Token NextToken()
        {
            int ch;

            for (ch = this.NextChar(); ch != -1 && char.IsWhiteSpace((char)ch); ch = this.NextChar())
            {
            }

            if (ch == -1)
                return null;

            char character = (char)ch;

            if (character == StringDelimeter)
                return this.NextString();

            if (separators.Contains(character.ToString()))
                return new Token(TokenType.Separator, character.ToString());

            if (operators.Contains(character.ToString()))
                return new Token(TokenType.Operator, character.ToString());

            if (char.IsDigit(character))
                return this.NextInteger(character);

            return this.NextName(character);
        }

        private Token NextName(char first)
        {
            string name = first.ToString();

            int ch;

            for (ch = this.NextChar(); ch != -1 && char.IsLetter((char)ch); ch = this.NextChar())
                name += (char)ch;

            this.PushChar(ch);

            return new Token(TokenType.Name, name);
        }

        private Token NextInteger(char first)
        {
            string name = first.ToString();

            int ch;

            for (ch = this.NextChar(); ch != -1 && char.IsDigit((char)ch); ch = this.NextChar())
                name += (char)ch;

            this.PushChar(ch);

            return new Token(TokenType.Integer, Convert.ToInt32(name, System.Globalization.CultureInfo.InvariantCulture));
        }

        private Token NextString()
        {
            string value = string.Empty;

            int ch;

            for (ch = this.NextStringChar(); ch != -1 && ch != -2; ch = this.NextStringChar())
                value += (char)ch;

            if (ch == -1)
                throw new LexerException("Unclosed string");

            return new Token(TokenType.String, value);
        }

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            return this.reader.Read();
        }

        private int NextStringChar()
        {
            int ch = this.NextChar();

            if (ch != -1 && (char)ch == StringEscape)
            {
                int ch2 = this.NextChar();

                if (ch2 == '\\')
                    return '\\';

                if (ch2 == '"')
                    return '"';

                if (ch2 == 'r')
                    return '\r';

                if (ch2 == 'n')
                    return '\n';

                this.PushChar(ch2);
            }

            if (ch == StringDelimeter)
                return -2; // special case

            return ch;
        }

        private void PushChar(int ch)
        {
            this.chars.Push(ch);
        }
    }
}

