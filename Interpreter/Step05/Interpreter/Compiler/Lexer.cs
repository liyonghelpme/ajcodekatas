namespace Interpreter.Compiler
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
        private static String[] separators = new string[] { ";" };
        private static String[] operators = new string[] { "=" };

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
                ;

            if (ch == -1)
                return null;

            char character = (char) ch;

            if (separators.Contains(character.ToString()))
                return new Token(TokenType.Separator, character.ToString());

            if (operators.Contains(character.ToString()))
                return new Token(TokenType.Operator, character.ToString());

            if (char.IsDigit(character))
                return NextInteger(character);

            return NextName(character);
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

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            return this.reader.Read();
        }

        private void PushChar(int ch)
        {
            this.chars.Push(ch);
        }
    }
}

