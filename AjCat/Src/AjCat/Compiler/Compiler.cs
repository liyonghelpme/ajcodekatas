namespace AjCat.Compiler
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjCat.Expressions;

    public class Compiler
    {
        private Parser parser;

        public Compiler(Parser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }

            this.parser = parser;
        }

        public Compiler(string text)
            : this(new Parser(text))
        {
        }

        public Compiler(TextReader reader)
            : this(new Parser(reader))
        {
        }

        public Expression CompileExpression()
        {
            List<Expression> list = CompileList();

            Token token = parser.NextToken();

            if (token != null)
            {
                throw new UnexpectedTokenException(token);
            }

            if (list == null || list.Count == 0)
            {
                return null;
            }

            if (list.Count == 1)
            {
                return list[0];
            }

            return new CompositeExpression(list);
        }

        private Expression CompileQuotation()
        {
            List<Expression> list = CompileList();

            Token token = parser.NextToken();

            if (token == null)
            {
                throw new ExpectedTokenException("]");
            }

            if (token.Value != "]")
            {
                throw new UnexpectedTokenException(token);
            }

            return new QuotationExpression(list);
        }

        private List<Expression> CompileList()
        {
            Token token = parser.NextToken();

            if (token == null)
            {
                return null;
            }

            List<Expression> list = new List<Expression>();

            parser.PushToken(token);

            while (!this.IsClosingToken(token))
            {
                Expression expression = this.CompileSingleExpression();
                list.Add(expression);
                token = parser.NextToken();
                if (token != null)
                    parser.PushToken(token);
            }

            return list;
        }

        private bool IsClosingToken(Token token)
        {
            if (token == null)
            {
                return true;
            }

            if (token.Value == "]")
            {
                return true;
            }

            return false;
        }

        private Expression CompileSingleExpression()
        {
            Token token = parser.NextToken();

            if (token==null) 
            {
                return null;
            }

            switch (token.TokenType) 
            {
                case TokenType.Integer:
                    return new IntegerExpression(Convert.ToInt32(token.Value));
                case TokenType.Name:
                    return Expressions.GetByName(token.Value);
                case TokenType.Separator:
                    if (token.Value == "[")
                        return CompileQuotation();
                    break;
            }

            throw new UnexpectedTokenException(token);
        }


    }
}
