namespace AjClipper.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using AjClipper.Commands;
    using AjClipper.Expressions;

    public class Parser
    {
        private Lexer lexer;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public Parser(TextReader reader)
            : this(new Lexer(reader))
        {
        }

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public ICommand ParseCommand()
        {
            this.SkipEmptyLines();
            ICommand command = this.ParseLineCommand();

            if (command != null)
                this.ParseEndOfLine();

            return command;
        }

        public IExpression ParseExpression()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.TokenType == TokenType.Name && token.Value == "new")
                return this.ParseNewExpression();

            if (token != null)
                this.lexer.PushToken(token);

            return this.ParseBinaryExpressionLevel0();
        }

        public ICommand ParseCommandList(params string[] terminators)
        {
            CompositeCommand commands = new CompositeCommand();

            Token token = this.lexer.NextToken();

            while (token != null && !terminators.Contains(token.Value))
            {
                this.lexer.PushToken(token);
                ICommand command = this.ParseCommand();

                if (command != null)
                    commands.AddCommand(command);

                token = this.lexer.NextToken();
            }

            if (token != null)
                this.lexer.PushToken(token);

            return commands;
        }

        private void SkipEmptyLines()
        {
            Token token = this.lexer.NextToken();

            while (token != null && token.TokenType == TokenType.EndOfLine)
                token = this.lexer.NextToken();

            if (token != null)
                this.lexer.PushToken(token);
        }

        private void ParseEndOfLine()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return;

            if (token.TokenType == TokenType.EndOfLine)
                return;

            throw new ParserException("Expected end of line");
        }

        private IExpression ParseBinaryExpressionLevel0()
        {
            IExpression expression = this.ParseBinaryExpressionLevel1();

            Token token = this.lexer.NextToken();

            while (token != null && token.TokenType == TokenType.Operator && (token.Value == ">" || token.Value == "<" || token.Value == "<=" || token.Value == ">=" || token.Value == "<>" || token.Value == "!=" || token.Value == "=="))
            {
                if (token.Value == ">")
                    expression = new CompareExpression(expression, this.ParseBinaryExpressionLevel1(), CompareOperator.Greater);
                else if (token.Value == "<")
                    expression = new CompareExpression(expression, this.ParseBinaryExpressionLevel1(), CompareOperator.Less);
                else if (token.Value == ">=")
                    expression = new CompareExpression(expression, this.ParseBinaryExpressionLevel1(), CompareOperator.GreaterEqual);
                else if (token.Value == "<=")
                    expression = new CompareExpression(expression, this.ParseBinaryExpressionLevel1(), CompareOperator.LessEqual);
                else if (token.Value == "<>" || token.Value == "!=")
                    expression = new CompareExpression(expression, this.ParseBinaryExpressionLevel1(), CompareOperator.NotEqual);
                else if (token.Value == "==")
                    expression = new CompareExpression(expression, this.ParseBinaryExpressionLevel1(), CompareOperator.Equal);

                token = this.lexer.NextToken();
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expression;
        }

        private IExpression ParseBinaryExpressionLevel1()
        {
            IExpression expression = this.ParseBinaryExpressionLevel2();

            Token token = this.lexer.NextToken();

            while (token != null && (token.Value == "+" || token.Value == "-"))
            {
                switch (token.Value[0])
                {
                    case '+':
                        expression = new AddExpression(expression, this.ParseBinaryExpressionLevel2());
                        break;
                    case '-':
                        expression = new SubtractExpression(expression, this.ParseBinaryExpressionLevel2());
                        break;
                }

                token = this.lexer.NextToken();
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expression;
        }

        private IExpression ParseBinaryExpressionLevel2()
        {
            IExpression expression = this.ParseSimpleExpression();

            Token token = this.lexer.NextToken();

            while (token != null && (token.Value == "*" || token.Value == "/"))
            {
                switch (token.Value[0])
                {
                    case '*':
                        expression = new MultiplyExpression(expression, this.ParseSimpleExpression());
                        break;
                    case '/':
                        expression = new DivideExpression(expression, this.ParseSimpleExpression());
                        break;
                }

                token = this.lexer.NextToken();
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expression;
        }

        private IExpression ParseSimpleExpression()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return null;

            switch (token.TokenType)
            {
                case TokenType.String:
                    return new ConstantExpression(token.Value);
                case TokenType.Integer:
                    return new ConstantExpression(Int32.Parse(token.Value));
                case TokenType.Name:
                    return this.ParseNameExpression(token.Value);
                case TokenType.Delimiter:
                    if (token.Value == "(")
                    {
                        bool skip = this.lexer.SkipEndOfLine;

                        this.lexer.SkipEndOfLine = true;

                        try
                        {
                            IExpression expression = this.ParseExpression();
                            this.ParseToken(")", TokenType.Delimiter);
                            return expression;
                        }
                        finally
                        {
                            this.lexer.SkipEndOfLine = skip;
                        }
                    }
                    break;
            }

            throw new ParserException(string.Format("Invalid expression: {0}", token.Value));
        }

        private IExpression ParseNameExpression(string name)
        {
            IExpression expression = new NameExpression(name);

            string newname = null;

            while (this.TryParse(".", TokenType.Operator))
            {
                if (newname != null)
                    expression = new DotExpression(expression, newname);
                newname = this.ParseName();
            }

            IList<IExpression> arguments = this.ParseArguments();

            if (arguments == null)
                if (newname == null)
                    return expression;
                else
                    return new DotExpression(expression, newname);

            if (newname == null)
                throw new ParserException("Simple invoke not yet supported");

            return new DotExpression(expression, newname, arguments);
        }

        private IExpression ParseComplexName()
        {
            string name = this.ParseName();

            IExpression expression = new NameExpression(name);

            while (this.TryParse(".", TokenType.Operator))
                expression = new DotExpression(expression, this.ParseName());

            return expression;
        }

        private ICommand ParseLineCommand()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Value == "?")
                return new PrintLineCommand(this.ParseExpressionList());

            if (token.TokenType == TokenType.Name)
            {
                token.Value = token.Value.ToLower();

                if (token.Value == "if")
                    return this.ParseIfCommand();

                if (token.Value == "while")
                    return this.ParseWhileCommand();

                if (token.Value == "procedure")
                    return this.ParseProcedureCommand();

                if (token.Value == "do")
                    return this.ParseDoProcedureCommand();

                if (token.Value == "public")
                    return this.ParsePublicCommand();

                if (token.Value == "use")
                    return this.ParseUseCommand();

                if (token.Value == "local")
                    return this.ParseLocalCommand();

                if (token.Value == "private")
                    return this.ParsePrivateCommand();

                Token token2 = this.lexer.NextToken();

                if (token2 != null && (token2.Value == ":=" || token2.Value == "="))
                    return new SetVariableCommand(token.Value, this.ParseExpression());
            }

            throw new ParserException(string.Format("Unexpected token {0}", token.Value));
        }

        private ICommand ParseWhileCommand()
        {
            IExpression condition = this.ParseExpression();
            this.ParseEndOfLine();
            ICommand command = this.ParseCommandList("end", "enddo");

            WhileCommand whileCommand = new WhileCommand(condition, command);

            this.lexer.NextToken();
            return whileCommand;
        }

        private ICommand ParsePublicCommand()
        {
            List<string> names = this.ParseNameList();

            return new PublicCommand(names);
        }

        private ICommand ParseLocalCommand()
        {
            List<string> names = this.ParseNameList();

            return new LocalCommand(names);
        }

        private ICommand ParsePrivateCommand()
        {
            List<string> names = this.ParseNameList();

            return new PrivateCommand(names);
        }

        private ICommand ParseUseCommand()
        {
            if (this.TryParse("database", TokenType.Name))
                return ParseUseDatabaseCommand();

            this.TryParse("workarea", TokenType.Name);

            return ParseUseWorkAreaCommand();
        }

        private ICommand ParseUseDatabaseCommand()
        {
            IExpression nameExpression = this.ParseExpression();

            IExpression connectionExpression = null;
            IExpression providerExpression = null;

            while (true)
            {
                if (this.TryParseName("connectionstring"))
                {
                    if (connectionExpression != null)
                        throw new ParserException("Connection String already speficied");

                    connectionExpression = this.ParseExpression();
                    continue;
                }

                if (this.TryParseName("provider"))
                {
                    if (providerExpression != null)
                        throw new ParserException("Provider already specified");

                    providerExpression = this.ParseExpression();

                    continue;
                }

                break;
            }

            return new UseDatabaseCommand(nameExpression, connectionExpression, providerExpression);
        }

        private ICommand ParseUseWorkAreaCommand()
        {
            IExpression nameExpression = this.ParseExpression();
            IExpression commandExpression = null;

            if (this.TryParse("command", TokenType.Name))
                commandExpression = this.ParseExpression();

            return new UseWorkAreaCommand(nameExpression, commandExpression);
        }

        private ICommand ParseDoProcedureCommand()
        {
            string name = this.ParseName();
            IList<IExpression> arguments = this.ParseArguments();

            return new DoProcedureCommand(name, arguments);
        }

        private IExpression ParseNewExpression()
        {
            IExpression nameExpression = this.ParseComplexName();
            IList<IExpression> arguments = this.ParseArguments();

            return new NewExpression(nameExpression, arguments);
        }

        private ICommand ParseProcedureCommand()
        {
            string name = this.ParseName();
            List<string> parameterNames = this.ParseParameterNameList();
            this.ParseEndOfLine();
            ICommand command = this.ParseCommandList("return");

            ProcedureCommand procedureCommand = new ProcedureCommand(name, parameterNames, command);

            this.lexer.NextToken();

            return procedureCommand;
        }

        private List<string> ParseParameterNameList()
        {
            List<string> names = new List<string>();

            Token token = this.lexer.NextToken();

            if (token == null)
                return names;

            if (token.Value != "(")
            {
                this.lexer.PushToken(token);
                return names;
            }

            string name;

            name = this.ParseName();
            names.Add(name);

            token = this.lexer.NextToken();

            while (token != null && token.Value == ",")
            {
                name = this.ParseName();
                names.Add(name);

                token = this.lexer.NextToken();
            }

            if (token == null || token.Value != ")")
                throw new ParserException("')' expected");

            return names;
        }

        private List<string> ParseNameList()
        {
            List<string> names = new List<string>();

            Token token = this.lexer.NextToken();

            if (token == null)
                return names;

            this.lexer.PushToken(token);

            string name;

            name = this.ParseName();
            names.Add(name);

            token = this.lexer.NextToken();

            while (token != null && token.Value == ",")
            {
                name = this.ParseName();
                names.Add(name);

                token = this.lexer.NextToken();
            }

            if (token != null)
                this.lexer.PushToken(token);

            return names;
        }

        private List<IExpression> ParseArguments()
        {
            List<IExpression> arguments = new List<IExpression>();

            Token token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Value != "(" || token.TokenType != TokenType.Delimiter)
            {
                this.lexer.PushToken(token);
                return null;
            }

            token = this.lexer.NextToken();

            if (token != null && token.TokenType == TokenType.Delimiter && token.Value == ")")
                return arguments;

            this.lexer.PushToken(token);

            while (token == null || token.TokenType != TokenType.Delimiter || token.Value != ")")
            {
                arguments.Add(this.ParseExpression());

                token = this.lexer.NextToken();

                if (token == null || token.TokenType != TokenType.Delimiter || token.Value != ",")
                    break;
            }

            if (token == null || token.TokenType != TokenType.Delimiter || token.Value != ")")
                throw new ParserException("')' expected");

            return arguments;
        }

        private ICommand ParseIfCommand()
        {
            IfCommand ifCommand = new IfCommand();

            Token token = new Token() { TokenType = TokenType.Name, Value = "elseif" };

            while (token != null && token.Value == "elseif")
            {
                IExpression condition = this.ParseExpression();
                this.ParseEndOfLine();
                ICommand command = this.ParseCommandList("endif", "elseif", "else", "end");
                ifCommand.AddConditionAndCommand(condition, command);
                token = this.lexer.NextToken();
            }

            if (token != null && token.Value == "else")
            {
                this.ParseEndOfLine();
                ICommand command = this.ParseCommandList("endif", "end");
                ifCommand.AddElseCommand(command);
            }

            this.lexer.NextToken();
            return ifCommand;
        }

        private string ParseName()
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.TokenType != TokenType.Name)
                throw new ParserException("Name expected");

            return token.Value;
        }

        private void ParseName(string expected)
        {
            if (this.ParseName() != expected)
                throw new ParserException(string.Format("Name '{0}' expected", expected));
        }

        private List<IExpression> ParseExpressionList()
        {
            List<IExpression> expressions = new List<IExpression>();

            IExpression expression = this.ParseExpression();

            if (expression == null)
                return expressions;

            expressions.Add(expression);

            while (this.TryParse(",", TokenType.Delimiter))
            {
                expression = this.ParseExpression();

                if (expression == null)
                    throw new ParserException("Invalid list of expressions");

                expressions.Add(expression);
            }

            return expressions;
        }

        private bool TryParseName(string value)
        {
            return this.TryParse(value, TokenType.Name);
        }

        private bool TryParse(string value)
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return false;

            if (!token.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                this.lexer.PushToken(token);
                return false;
            }

            return true;
        }

        private bool TryParse(string value, TokenType type)
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return false;

            if (!token.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase) || token.TokenType != type)
            {
                this.lexer.PushToken(token);
                return false;
            }

            return true;
        }

        private void ParseToken(string value, TokenType type)
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                throw new LexerException(string.Format("Expected '{0}'", value));

            if (token.Value != value || token.TokenType != type)
                throw new LexerException(string.Format("Unexpected '{0}'", token.Value));
        }
    }
}
