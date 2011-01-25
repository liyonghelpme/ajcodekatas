using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interpreter.Compiler;
using Interpreter.Expressions;
using Interpreter.Commands;

namespace Interpreter.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseIntegerExpression()
        {
            Parser parser = new Parser("1");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.AreEqual(1, expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseStringExpression()
        {
            Parser parser = new Parser("\"foo\"");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));
            Assert.AreEqual("foo", expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException), "Unclosed string")]
        public void RaiseIfUnclosedStringExpression()
        {
            Parser parser = new Parser("\"foo");

            IExpression expression = parser.ParseExpression();
        }

        [TestMethod]
        public void ParseVariableExpression()
        {
            Parser parser = new Parser("one");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(VariableExpression));

            VariableExpression varexpr = (VariableExpression)expression;

            Assert.AreEqual("one", varexpr.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateAddExpression()
        {
            Parser parser = new Parser("1+2");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            BinaryArithmeticExpression addexpr = (BinaryArithmeticExpression)expression;

            Assert.AreEqual(ArithmeticOperator.Add, addexpr.Operator);

            Assert.AreEqual(3, addexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateMultiplyExpression()
        {
            Parser parser = new Parser("2*3");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            BinaryArithmeticExpression multexpr = (BinaryArithmeticExpression)expression;

            Assert.AreEqual(ArithmeticOperator.Multiply, multexpr.Operator);

            Assert.AreEqual(6, multexpr.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateAddAndMultiplyExpression()
        {
            Parser parser = new Parser("1+2*3");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            Assert.AreEqual(7, expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateAddAndMultiplyExpressionWithParenthesis()
        {
            Parser parser = new Parser("(1+2)*3");

            IExpression expression = parser.ParseExpression();
            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(BinaryArithmeticExpression));

            Assert.AreEqual(9, expression.Evaluate(null));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleSetCommand()
        {
            Parser parser = new Parser("a = 1;");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SetCommand));

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.AreEqual(1, environment.GetValue("a"));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfCommandHasNoClosing()
        {
            Parser parser = new Parser("a = 1");

            ICommand command = parser.ParseCommand();
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleIfCommand()
        {
            Parser parser = new Parser("if (a) b=1; else b=2;");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.AreEqual(2, environment.GetValue("b"));            
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleWhileCommand()
        {
            Parser parser = new Parser("while (a) a = a-1;");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(WhileCommand));

            BindingEnvironment environment = new BindingEnvironment();
            environment.SetValue("a", 2);

            command.Execute(environment);

            Assert.AreEqual(0, environment.GetValue("a"));
        }

        [TestMethod]
        public void ParseCompositeCommand()
        {
            Parser parser = new Parser("{ a = a+1; a = a+2; }");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(CompositeCommand));

            CompositeCommand ccmd = (CompositeCommand)command;

            Assert.IsNotNull(ccmd.Commands);
            Assert.AreEqual(2, ccmd.Commands.Count);
            Assert.IsInstanceOfType(ccmd.Commands.First(), typeof(SetCommand));
            Assert.IsInstanceOfType(ccmd.Commands.Skip(1).First(), typeof(SetCommand));
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfCompositeCommandNotClosed()
        {
            Parser parser = new Parser("{ a = a+1; a = a+2; ");

            parser.ParseCommand();
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleIfWithCompositeCommand()
        {
            Parser parser = new Parser("if (1) { a = a+1; a = a+2; }");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(IfCommand));

            IfCommand ifcmd = (IfCommand)command;

            Assert.IsInstanceOfType(ifcmd.ThenCommand, typeof(CompositeCommand));

            BindingEnvironment environment = new BindingEnvironment();
            environment.SetValue("a", 2);

            command.Execute(environment);

            Assert.AreEqual(5, environment.GetValue("a"));
        }

        [TestMethod]
        public void ParseAndEvaluateSimpleWhileWithCompositeCommand()
        {
            Parser parser = new Parser("while (a) { a = a-1; b=b+1; }");

            ICommand command = parser.ParseCommand();

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(WhileCommand));

            WhileCommand whilecmd = (WhileCommand)command;

            Assert.IsInstanceOfType(whilecmd.Command, typeof(CompositeCommand));

            BindingEnvironment environment = new BindingEnvironment();
            environment.SetValue("a", 2);
            environment.SetValue("b", 0);

            command.Execute(environment);

            Assert.AreEqual(0, environment.GetValue("a"));
            Assert.AreEqual(2, environment.GetValue("b"));
        }
    }
}
