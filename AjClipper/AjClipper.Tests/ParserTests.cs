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
        [TestMethod]
        public void ShouldParsePrintLineCommand()
        {
            Parser parser = new Parser("? \"Hello World\"");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(PrintLineCommand));
        }

        [TestMethod]
        public void ShouldParseAndExecutePrintLineCommand()
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
        public void ShouldParseAndExecutePrintLineCommandWithListOfExpressions()
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
        public void ShouldParseAndExecuteSetVariableCommand()
        {
            Parser parser = new Parser("foo := \"bar\"");

            ICommand command = parser.ParseCommand();

            ValueEnvironment environment = new ValueEnvironment();

            command.Execute(null, environment);

            Assert.AreEqual("bar", environment.GetValue("foo"));
        }

        [TestMethod]
        public void ShouldParseSimpleIfCommand()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteSimpleIfCommand()
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
        public void ShouldParseIfCommandWithMultipleCommands()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\n  b:=1\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteIfCommandWithMultipleCommands()
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
        public void ShouldSkipIfCommandsIfFalse()
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
        public void ShouldParseIfCommandWithElse()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelse\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteIfCommandWithElse()
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
        public void ShouldParseIfCommandWithElseIf()
        {
            Parser parser = new Parser("if 0\r\n  a:=1\r\nelseif 1\r\n  a:=2\r\nendif");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteIfCommandWithElseIf()
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
        public void ShouldParseSimpleWhile()
        {
            Parser parser = new Parser("while 1\r\n a:=1\r\nenddo");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(WhileCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ShouldExecuteSimpleWhile()
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
        public void ShouldParseAndEvaluateIntegerExpression()
        {
            Parser parser = new Parser("123");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(((ConstantExpression)expression).Evaluate(null), typeof(int));
            Assert.AreEqual(123, (int)((ConstantExpression)expression).Evaluate(null));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateStringExpression()
        {
            Parser parser = new Parser("\"foo\"");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(((ConstantExpression)expression).Evaluate(null), typeof(string));
            Assert.AreEqual("foo", (string)((ConstantExpression)expression).Evaluate(null));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateNameExpression()
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
        public void ShouldParseAndEvaluateAddExpression()
        {
            Parser parser = new Parser("1+2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(3, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateAddVariablesExpression()
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
        public void ShouldParseAndEvaluateSubtractExpression()
        {
            Parser parser = new Parser("1-2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(SubtractExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(-1, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateSubtractVariablesExpression()
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
        public void ShouldParseAndEvaluateArithmeticExpression()
        {
            Parser parser = new Parser("1+2*3");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(int));
            Assert.AreEqual(7, (int)(expression.Evaluate(null)));
        }

        [TestMethod]
        public void ShouldParseSimpleProcedure()
        {
            Parser parser = new Parser("Procedure DoBar\r\na := 1\r\nreturn");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(ProcedureCommand));
        }

        [TestMethod]
        public void ShouldParseSimpleProcedureWithParameter()
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
        public void ShouldParseSimpleProcedureWithTwoParameters()
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
        public void ShouldRaiseIfIncompleteParameterList()
        {
            Parser parser = new Parser("Procedure DoBar(b,c\r\na := b+c\r\nreturn");

            parser.ParseCommand();
        }

        [TestMethod]
        public void ShouldParseAndEvaluateSimpleEqualExpression()
        {
            Parser parser = new Parser("1==2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateSimpleGreaterExpression()
        {
            Parser parser = new Parser("1>2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateGreaterExpression()
        {
            Parser parser = new Parser("1+3>2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateSimpleLessExpression()
        {
            Parser parser = new Parser("1<2");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(CompareExpression));
            Assert.IsInstanceOfType(expression.Evaluate(null), typeof(bool));
            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void ShouldParseAndEvaluateLessExpression()
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

        private static bool ParseAndEvaluateBoolean(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            return (bool)expression.Evaluate(null);
        }
    }
}
