using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Patterns.Adapter
{
    public class Lexer : ILexer
    {
        private TextReader reader;
        private Stack<char> characters = new Stack<char>();
        private static string operators = "=+-*/";
        private static string separators = ";()";

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public Token ReadToken()
        {
            this.SkipWhitespaces();
            int ch = this.ReadChar();

            if (ch == -1)
                return null;

            char character = (char)ch;

            if (char.IsLetter(character))
                return this.ReadName(character);

            if (char.IsDigit(character))
                return this.ReadInteger(character);

            if (operators.Contains(character))
                return new Token() { TokenType = TokenType.Operator, Value = character.ToString() };

            if (separators.Contains(character))
                return new Token() { TokenType = TokenType.Separator, Value = character.ToString() };

            throw new InvalidDataException();
        }

        private int ReadChar()
        {
            if (this.characters.Count > 0)
                return this.characters.Pop();

            return this.reader.Read();
        }

        private void SkipWhitespaces()
        {
            int ch = this.ReadChar();

            while (ch != -1 && char.IsWhiteSpace((char)ch))
                ch = this.ReadChar();

            if (ch != -1)
                this.PushChar((char) ch);
        }

        private Token ReadName(char character)
        {
            string value = character.ToString();
            int ch;

            for (ch = this.ReadChar(); ch != -1 && char.IsLetterOrDigit((char)ch); ch = this.ReadChar())
                value += (char)ch;

            if (ch != -1)
                this.PushChar((char) ch);

            return new Token() { TokenType = TokenType.Name, Value = value };
        }

        private Token ReadInteger(char character)
        {
            string value = character.ToString();
            int ch;

            for (ch = this.ReadChar(); ch != -1 && char.IsDigit((char)ch); ch = this.ReadChar())
                value += (char)ch;

            if (ch != -1)
                this.PushChar((char)ch);

            return new Token() { TokenType = TokenType.Integer, Value = Convert.ToInt32(value) };
        }

        private void PushChar(char ch)
        {
            this.characters.Push(ch);
        }
    }
}
