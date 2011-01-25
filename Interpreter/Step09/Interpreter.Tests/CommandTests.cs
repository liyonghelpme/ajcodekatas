using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Interpreter.Commands;
using Interpreter.Expressions;

namespace Interpreter.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void CreateSetCommand()
        {
            SetCommand command = new SetCommand("foo", new ConstantExpression(1));

            Assert.AreEqual("foo", command.Name);
            Assert.IsInstanceOfType(command.Expression, typeof(ConstantExpression));
        }

        [TestMethod]
        public void ExecuteSetCommand()
        {
            BindingEnvironment environment = new BindingEnvironment();
            ICommand command = new SetCommand("one", new ConstantExpression(1));

            command.Execute(environment);

            Assert.AreEqual(1, environment.GetValue("one"));
        }

        [TestMethod]
        public void CreateIfCommand()
        {
            IExpression condition = new ConstantExpression(0);
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            Assert.AreEqual(condition, command.Condition);
            Assert.AreEqual(thenCommand, command.ThenCommand);
            Assert.AreEqual(elseCommand, command.ElseCommand);
        }

        [TestMethod]
        public void EvaluateIfCommandWithZeroAsCondition()
        {
            IExpression condition = new ConstantExpression(0);
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.IsNull(environment.GetValue("a"));
            Assert.AreEqual(2, environment.GetValue("b"));
        }

        [TestMethod]
        public void EvaluateIfCommandWithNonEmptyStringAsCondition()
        {
            IExpression condition = new ConstantExpression("foo");
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.IsNull(environment.GetValue("b"));
            Assert.AreEqual(1, environment.GetValue("a"));
        }

        [TestMethod]
        public void EvaluateIfCommandWithNullAsCondition()
        {
            IExpression condition = new ConstantExpression(null);
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.IsNull(environment.GetValue("a"));
            Assert.AreEqual(2, environment.GetValue("b"));
        }

        [TestMethod]
        public void EvaluateIfCommandWithNonZeroAsCondition()
        {
            IExpression condition = new ConstantExpression(1);
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.IsNull(environment.GetValue("b"));
            Assert.AreEqual(1, environment.GetValue("a"));
        }

        [TestMethod]
        public void EvaluateIfCommandWithFalseAsCondition()
        {
            IExpression condition = new ConstantExpression(false);
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.IsNull(environment.GetValue("a"));
            Assert.AreEqual(2, environment.GetValue("b"));
        }

        [TestMethod]
        public void EvaluateIfCommandWithTrueAsCondition()
        {
            IExpression condition = new ConstantExpression(true);
            ICommand thenCommand = new SetCommand("a", new ConstantExpression(1));
            ICommand elseCommand = new SetCommand("b", new ConstantExpression(2));

            IfCommand command = new IfCommand(condition, thenCommand, elseCommand);

            BindingEnvironment environment = new BindingEnvironment();

            command.Execute(environment);

            Assert.IsNull(environment.GetValue("b"));
            Assert.AreEqual(1, environment.GetValue("a"));
        }

        [TestMethod]
        public void EvaluateWhileCommandUsingDecrement()
        {
            BindingEnvironment environment = new BindingEnvironment();
            environment.SetValue("a", 2);
            IExpression condition = new VariableExpression("a");

            ICommand command = new SetCommand("a", new BinaryArithmeticExpression(new VariableExpression("a"), new ConstantExpression(1), ArithmeticOperator.Subtract));

            WhileCommand wcommand = new WhileCommand(condition, command);

            Assert.AreEqual(command, wcommand.Command);
            Assert.AreEqual(condition, wcommand.Condition);

            wcommand.Execute(environment);

            Assert.AreEqual(0, environment.GetValue("a"));
        }

        [TestMethod]
        public void CreateAndEvaluateCompositeCommand()
        {
            BindingEnvironment environment = new BindingEnvironment();
            environment.SetValue("a", 0);
            ICommand add1 = this.MakeAddCommand("a", "a", 1);
            ICommand add2 = this.MakeAddCommand("a", "a", 2);
            CompositeCommand cmd = new CompositeCommand(new ICommand[] { add1, add2 });

            Assert.IsNotNull(cmd.Commands);
            Assert.AreEqual(2, cmd.Commands.Count);
            Assert.AreEqual(add1, cmd.Commands.First());
            Assert.AreEqual(add2, cmd.Commands.Skip(1).First());

            cmd.Execute(environment);

            Assert.AreEqual(3, environment.GetValue("a"));
        }

        private ICommand MakeAddCommand(string target, string source, int number)
        {
            IExpression add = new BinaryArithmeticExpression(new VariableExpression(source), new ConstantExpression(number), ArithmeticOperator.Add);
            ICommand set = new SetCommand(target, add);
            return set;
        }
    }
}
