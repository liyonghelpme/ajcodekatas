namespace WebScrapping
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class HtmlParser
    {
        private TextReader reader;
        private Stack<char> chars = new Stack<char>();
        private Queue<HtmlToken> tokens = new Queue<HtmlToken>();

        private State state = State.Text;
        private string tagname;

        public HtmlParser(TextReader reader)
        {
            this.reader = reader;
        }

        public HtmlParser(string text)
            : this(new StringReader(text))
        {
        }

        private enum State
        {
            Text,
            Tag
        }

        public HtmlToken NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Dequeue();

            if (this.state == State.Tag)
                return this.NextTagToken();

            char ch;

            try 
            {
                ch = this.NextChar();
            }
            catch (EndOfInputException) 
            {
                return null;
            }

            if (ch == '<')
                return this.NextTag();

            return this.NextText(ch);
        }

        private HtmlToken NextTagToken()
        {
            string word = this.NextWord();

            if (word == ">")
            {
                this.state = State.Text;

                return this.NextToken();
            }

            if (word == "/>")
            {
                this.state = State.Text;
                return new HtmlToken() { Name = this.tagname, TokenType = HtmlTokenType.CloseTag };
            }
            else if (string.IsNullOrEmpty(word) || !char.IsLetter(word[0]))
                throw new InvalidDataException();

            string value = this.NextValue();

            return new HtmlToken() { TokenType = HtmlTokenType.Attribute, Name = word.ToLower(), Value = value };
        }

        private HtmlToken NextText(char firstchar)
        {
            string text = firstchar.ToString();

            try 
            {
                char ch;

                for (ch = this.NextChar(); ch != '<'; ch = this.NextChar())
                    text += ch;

                this.PushChar(ch);
            }
            catch (EndOfInputException)
            {
            }

            return new HtmlToken() { TokenType = HtmlTokenType.Text, Value = text };
        }

        private HtmlToken NextTag()
        {
            this.state = State.Tag;

            string name = string.Empty;

            char ch = this.NextChar();

            if (ch == '!')
            {
                for (ch = this.NextChar(); ch != '>'; ch = this.NextChar())
                    ;

                this.state = State.Text;

                return this.NextToken();
            }

            bool isclose = ch == '/';

            if (isclose)
                ch = this.NextChar();

            while (char.IsLetterOrDigit(ch))
            {
                name += ch;
                ch = this.NextChar();
            }

            this.tagname = name.ToLower();

            if (isclose)
            {
                while (ch != '>')
                    ch = this.NextChar();
                this.state = State.Text;

                return new HtmlToken() { TokenType = HtmlTokenType.CloseTag, Name = tagname };
            }

            if (ch == '>')
                this.state = State.Text;
            else if (ch == '/')
            {
                ch = this.NextChar();

                if (ch == '>')
                {
                    this.AddToken(new HtmlToken() { Name = this.tagname, TokenType = HtmlTokenType.CloseTag });
                    this.state = State.Text;
                }
                else
                {
                    this.PushChar(ch);
                    this.PushChar('/');
                }
            }

            return new HtmlToken() { Name = this.tagname, TokenType = HtmlTokenType.Tag };
        }

        private string NextWord()
        {
            char ch = this.NextChar();

            while (char.IsWhiteSpace(ch))
                ch = this.NextChar();

            if (ch == '>')
                return ch.ToString();

            if (ch == '/')
            {
                char ch2 = this.NextChar();

                if (ch2 == '>')
                    return "/>";

                this.PushChar(ch2);
            }

            if (!char.IsLetter(ch))
                return ch.ToString();

            string word = ch.ToString();

            for (ch = this.NextChar(); char.IsLetterOrDigit(ch) || ch == '-'; ch = this.NextChar())
                word += ch;

            this.PushChar(ch);

            return word;
        }

        private string NextValue()
        {
            char ch = this.NextChar();

            while (char.IsWhiteSpace(ch))
                ch = this.NextChar();

            if (ch != '=')
            {
                this.PushChar(ch);
                return null;
            }

            ch = this.NextChar();

            while (char.IsWhiteSpace(ch))
                ch = this.NextChar();

            char delimiter = (char) 0;
            string value = string.Empty;

            if (ch == '\'' || ch == '"')
                delimiter = ch;
            else
                value += ch;

            for (ch = this.NextChar(); (delimiter != 0 || char.IsLetterOrDigit(ch)) && ch != delimiter; ch = this.NextChar())
                value += ch;

            if (ch != delimiter)
                this.PushChar(ch);

            return value;
        }

        private char NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            int nextch = this.reader.Read();

            if (nextch == -1)
                throw new EndOfInputException();

            return (char)nextch;
        }

        private void AddToken(HtmlToken token)
        {
            this.tokens.Enqueue(token);
        }

        private void PushChar(char ch)
        {
            this.chars.Push(ch);
        }
    }
}
