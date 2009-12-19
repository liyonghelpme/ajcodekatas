namespace AjClipper.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AjClipper.Expressions;
    using AjClipper.Commands;
    using AjClipper.Compiler;

    [TestClass]
    public class ParserTests
    {
        private const string OleDbProviderFactoryName = "System.Data.OleDb";
        private const string OleDbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;";

        [TestMethod]
        public void ParsePrintLineCommand()
        {
            Parser parser = new Parser("? \"Hello World\"");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(PrintLineCommand));
        }

        [TestMethod]
        public void ParseAndExecutePrintLineCommand()
        {
            Parser parser = new Parser("? \"Hello World\"");

            ICommand command = parser.ParseCommand();

            StringWriter writer = new StringWriter();

            lock (System.Console.Out)
            {
                TextWriter originalWriter = System.Console.Out;
                System.Console.SetOut(writer);

                command.Execute(null, null);

                System.Console.SetOut(originalWriter);
            }

            Assert.AreEqual("Hello World\r\n", writer.ToString());
        }

        [TestMethod]
        public void ParseAndExecutePrintLineCommandWithListOfExpressions()
        {
            Parser parser = new Parser("? \"Hello\", \" \", \"World\"");

            ICommand command = parser.ParseCommand();

            StringWriter writer = new StringWriter();

            lock (System.Console.Out)
            {
                TextWriter originalWriter = System.Console.Out;
                System.Console.SetOut(writer);

                command.Execute(null, null);

                System.Console.SetOut(originalWriter);
            }

            Assert.AreEqual("Hello World\r\n", writer.ToString());
        }

        [TestMethod]
        public void ParseAndExecuteSetVariableCommand()
        {
            Parser parser = new Parser("foo := \"bar\"");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void ParseSetVariableToExpressionCommand()
        {
            Parser parser = new Parser("foo := bar.Calculate(1,2)");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetVariableCommand));
        }

        [TestMethod]
        public void ParseSimpleIfCommand()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ExecuteSimpleIfCommand()
        {
            Parser parser = new Parser("if 1\r\n  a:=1\r\nendif");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(1, (int)value);
        }

        [TestMethod]
        public void ParseIfCommandWithMultipleCommands()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\n  b:=1\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ExecuteIfCommandWithMultipleCommands()
        {
            Parser parser = new Parser("if 1\r\n  a:=1\r\n  b:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(1, (int)value);

            value = environment.GetValue("b");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(2, (int)value);
        }

        [TestMethod]
        public void SkipIfCommandsIfFalse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\n  b:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNull(value);

            value = environment.GetValue("b");

            Assert.IsNull(value);
        }

        [TestMethod]
        public void ParseIfCommandWithElse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelse\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ExecuteIfCommandWithElse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelse\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(2, (int)value);
        }

        [TestMethod]
        public void ParseIfCommandWithElseIf()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelseif 1\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ExecuteIfCommandWithElseIf()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelseif 1\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(2, (int)value);
        }

        [TestMethod]
        public void ParseSimpleWhile()
        {
            Parser parser = new Parser("while 1\r\n a:=1\r\nenddo");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(WhileCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ExecuteSimpleWhile()
        {
            Parser parser = new Parser("while a\r\n a:=0\r\nenddo");

            ICommand command = parser.ParseCommand();
            ValueEnvironment environment = new ValueEnvironment();
            environment.SetValue("a", 1);

            command.Execute(null, environment);

            object value = environment.GetValue("a");

            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(0, (int)value);
        }

        [TestMethod]
        public void ParseAndEvaluateIntegerExpression()
        {
            Parser parser = new Parser("123");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(((ConstantExpression)expression).Evaluate(null), typeof(int));
            Assert.AreEqual(123, (int)((ConstantExpression)expression).Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateIntegerExpressionWithParenthesis()
        {
            Parser parser = new Parser("(123)");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(((ConstantExpression)expression).Evaluate(null), typeof(int));
            Assert.AreEqual(123, (int)((ConstantExpression)expression).Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateStringExpression()
        {
            Parser parser = new Parser("\"foo\"");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(((ConstantExpression)expression).Evaluate(null), typeof(string));
            Assert.AreEqual("foo", (string)((ConstantExpression)expression).Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateNameExpression()
        {
            Parser parser = new Parser("foo");

            IExpression expression = parser.ParseExpression();
            ValueEnvironment environment = new ValueEnvironment();
            environment.SetValue("foo", "bar");

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(NameExpression));
            Assert.IsInstanceOfType(((NameExpression)expression).Evaluate(environment), typeof(string));
            Assert.AreEqual("bar", (string)((NameExpression)expression).Evaluate(environment));
        }

        [TestMethod]
        public void ParseAndEvaluateAddExpression()
        {
            Parser parser = new Parser("1+2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(3, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ParseAndEvaluateAddExpressionWithParenthesis()
        {
            Parser parser = new Parser("(1+2)");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(3, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ParseAndEvaluateAddVariablesExpression()
        {
            Parser parser = new Parser("a+b");

            IExpression expression = parser.ParseExpression();
            ValueEnvironment environment = new ValueEnvironment();
            environment.SetValue("a", 1);
            environment.SetValue("b", 2);

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));
            Assert.IsInstanceOfType(expression.Evaluate(environment), typeof(int));
            Assert.AreEqual(3, (int)(expression.Evaluate(environment)));
        }

        [TestMethod]
        public void ParseAndEvaluateSubtractExpression()
        {
            Parser parser = new Parser("1-2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(SubtractExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(-1, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ParseAndEvaluateSubtractVariablesExpression()
        {
            Parser parser = new Parser("a-b");

            IExpression expression = parser.ParseExpression();
            ValueEnvironment environment = new ValueEnvironment();
            environment.SetValue("a", 1);
            environment.SetValue("b", 2);

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(SubtractExpression));
            Assert.IsInstanceOfType(expression.Evaluate(environment), typeof(int));
            Assert.AreEqual(-1, (int)(expression.Evaluate(environment)));
        }

        [TestMethod]
        public void ParseAndEvaluateArithmeticExpression()
        {
            Parser parser = new Parser("1+2*3");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(7, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ParseAndEvaluateArithmeticExpressionWithParenthesis()
        {
            Parser parser = new Parser("(1+2)*3");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MultiplyExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(9, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ParseSimpleProcedure()
        {
            Parser parser = new Parser("Procedure DoBar\r\na := 1\r\nreturn");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(ProcedureCommand));
        }

        [TestMethod]
        public void ParseSimpleProcedureWithParameter()
        {
            Parser parser = new Parser("Procedure DoBar(b)\r\na := b\r\nreturn");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(ProcedureCommand));

            ProcedureCommand proc = (ProcedureCommand)command;

            Assert.AreEqual("dobar", proc.Name);
            Assert.IsNotNull(proc.ParameterNames);
            Assert.AreEqual(1, proc.ParameterNames.Count);
            Assert.AreEqual("b", proc.ParameterNames[0]);
        }

        [TestMethod]
        public void ParseSimpleProcedureWithTwoParameters()
        {
            Parser parser = new Parser("Procedure DoBar(b,c)\r\na := b+c\r\nreturn");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(ProcedureCommand));

            ProcedureCommand proc = (ProcedureCommand)command;

            Assert.AreEqual("dobar", proc.Name);
            Assert.IsNotNull(proc.ParameterNames);
            Assert.AreEqual(2, proc.ParameterNames.Count);
            Assert.AreEqual("b", proc.ParameterNames[0]);
            Assert.AreEqual("c", proc.ParameterNames[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfIncompleteParameterList()
        {
            Parser parser = new Parser("Procedure DoBar(b,c\r\na := b+c\r\nreturn");

            parser.ParseCommand();
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleEqualExpression()
        {
            Parser parser = new Parser("1==2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleGreaterExpression()
        {
            Parser parser = new Parser("1>2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateGreaterExpression()
        {
            Parser parser = new Parser("1+3>2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleLessExpression()
        {
            Parser parser = new Parser("1<2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateLessExpression()
        {
            Parser parser = new Parser("1+3<2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void ParseAndEvaluateIntegerComparisons()
        {
            Assert.IsTrue(ParseAndEvaluateBoolean("1<2"));
            Assert.IsTrue(ParseAndEvaluateBoolean("2==2"));
            Assert.IsTrue(ParseAndEvaluateBoolean("2>1"));

            Assert.IsTrue(ParseAndEvaluateBoolean("1<=2"));
            Assert.IsFalse(ParseAndEvaluateBoolean("2<>2"));
            Assert.IsFalse(ParseAndEvaluateBoolean("2!=2"));
            Assert.IsTrue(ParseAndEvaluateBoolean("2>=1"));
        }

        [TestMethod]
        public void ParseAndEvaluateStringComparisons()
        {
            Assert.IsTrue(ParseAndEvaluateBoolean("\"bar\"<\"foo\""));
            Assert.IsTrue(ParseAndEvaluateBoolean("\"bar\"==\"bar\""));
            Assert.IsFalse(ParseAndEvaluateBoolean("\"bar\">\"foo\""));

            Assert.IsFalse(ParseAndEvaluateBoolean("\"foo\"<=\"bar\""));
            Assert.IsFalse(ParseAndEvaluateBoolean("\"bar\"<>\"bar\""));
            Assert.IsFalse(ParseAndEvaluateBoolean("\"bar\"!=\"bar\""));
            Assert.IsFalse(ParseAndEvaluateBoolean("\"bar\">=\"foo\""));
        }

        [TestMethod]
        public void ParseDoProcedure()
        {
            Parser parser = new Parser("do FooProcedure()");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(DoProcedureCommand));

            DoProcedureCommand docmd = (DoProcedureCommand)command;

            Assert.AreEqual("fooprocedure", docmd.Name);
            Assert.IsNotNull(docmd.Arguments);
            Assert.AreEqual(0, docmd.Arguments.Count);
        }

        [TestMethod]
        public void ParseDoProcedureWithArguments()
        {
            Parser parser = new Parser("do FooProcedure(1, 2)");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(DoProcedureCommand));

            DoProcedureCommand docmd = (DoProcedureCommand)command;

            Assert.AreEqual("fooprocedure", docmd.Name);
            Assert.IsNotNull(docmd.Arguments);
            Assert.AreEqual(2, docmd.Arguments.Count);
        }

        [TestMethod]
        public void ParsePublicVariables()
        {
            Parser parser = new Parser("public foo, bar");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(PublicCommand));

            PublicCommand pubcmd = (PublicCommand)command;

            Assert.AreEqual(2, pubcmd.Names.Count);
            Assert.AreEqual("foo", pubcmd.Names.First());
            Assert.AreEqual("bar", pubcmd.Names.Skip(1).First());
        }

        [TestMethod]
        public void ParseLocalVariables()
        {
            Parser parser = new Parser("local foo, bar");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(LocalCommand));

            LocalCommand localcmd = (LocalCommand)command;

            Assert.AreEqual(2, localcmd.Names.Count);
            Assert.AreEqual("foo", localcmd.Names.First());
            Assert.AreEqual("bar", localcmd.Names.Skip(1).First());
        }

        [TestMethod]
        public void ParsePrivateVariables()
        {
            Parser parser = new Parser("private foo, bar");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(PrivateCommand));

            PrivateCommand privatecmd = (PrivateCommand)command;

            Assert.AreEqual(2, privatecmd.Names.Count);
            Assert.AreEqual("foo", privatecmd.Names.First());
            Assert.AreEqual("bar", privatecmd.Names.Skip(1).First());
        }

        [TestMethod]
        public void ParseNewExpressionWithoutParameters()
        {
            Parser parser = new Parser("new System.Int32()");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(NewExpression));

            NewExpression newexpr = (NewExpression)expression;

            Assert.AreEqual("System.Int32", newexpr.TypeName);
            Assert.AreEqual(0, newexpr.Arguments.Count);
        }

        [TestMethod]
        public void ParseNewExpression()
        {
            Parser parser = new Parser("new System.IO.FileInfo(filename)");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(NewExpression));

            NewExpression newexpr = (NewExpression)expression;

            Assert.AreEqual("System.IO.FileInfo", newexpr.TypeName);
            Assert.AreEqual(1, newexpr.Arguments.Count);
        }

        [TestMethod]
        public void ParseUseDatabaseCommand()
        {
            Parser parser = new Parser(string.Format("use database TestDb connectionstring \"{0}\" provider \"{1}\"", OleDbConnectionString, OleDbProviderFactoryName));

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(UseDatabaseCommand));
        }

        [TestMethod]
        public void ParseUseWorkAreaCommand()
        {
            Parser parser = new Parser("use Test");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(UseWorkAreaCommand));
        }

        [TestMethod]
        public void ParseSimpleDotExpression()
        {
            IExpression expression = ParseExpression("a.Length");

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DotExpression));
        }

        [TestMethod]
        public void ParseSimpleDotExpressionWithArguments()
        {
            IExpression expression = ParseExpression("foo.Bar(1,2)");

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DotExpression));

            DotExpression dotexp = (DotExpression)expression;

            Assert.IsNotNull(dotexp.Arguments);
            Assert.AreEqual(2, dotexp.Arguments.Count);
        }

        [TestMethod]
        public void ParseSimpleDotExpressionWithNoArguments()
        {
            IExpression expression = ParseExpression("foo.Bar()");

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DotExpression));

            DotExpression dotexp = (DotExpression)expression;

            Assert.IsNull(dotexp.Arguments);
        }

        [TestMethod]
        public void ParseAndExecuteDotExpressionWithArguments()
        {
            ValueEnvironment environment = new ValueEnvironment();
            environment.SetValue("dinfo", new System.IO.DirectoryInfo("."));
            IExpression expression = ParseExpression("dinfo.GetFiles(\"*.exe\")");
            object result = expression.Evaluate(environment);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfUnexpectedTokenDot()
        {
            ParseExpression(".");
        }

        private static bool ParseAndEvaluateBoolean(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            return (bool)expression.Evaluate(null);
        }

        private static IExpression ParseExpression(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();

            Assert.IsNull(parser.ParseExpression());

            return expression;
        }
    }
}
