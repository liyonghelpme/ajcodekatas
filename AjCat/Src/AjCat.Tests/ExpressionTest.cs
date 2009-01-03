using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjCat;
using AjCat.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjCat.Tests
{
    [TestClass]
    public class ExpressionTest
    {
        [TestMethod]
        public void CreateIntegerExpression()
        {
            IntegerExpression expression = new IntegerExpression(10);

            Assert.AreEqual(10, expression.Value);
        }

        [TestMethod]
        public void CreateAndEvaluateIntegerExpression()
        {
            IntegerExpression expression = new IntegerExpression(10);
            Machine machine = new Machine();

            expression.Evaluate(machine);

            Assert.AreEqual(10, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateIntegerAddExpression()
        {
            IntegerAddOperation expression = IntegerAddOperation.Instance;

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);

            expression.Evaluate(machine);

            Assert.AreEqual(3, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateIntegerSubtractExpression()
        {
            IntegerSubtractOperation expression = IntegerSubtractOperation.Instance;

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);

            expression.Evaluate(machine);

            Assert.AreEqual(-1, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateIntegerMultiplyExpression()
        {
            IntegerMultiplyOperation expression = IntegerMultiplyOperation.Instance;

            Machine machine = new Machine();

            machine.Push(3);
            machine.Push(2);

            expression.Evaluate(machine);

            Assert.AreEqual(6, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateIntegerDivideExpression()
        {
            IntegerDivideOperation expression = IntegerDivideOperation.Instance;

            Machine machine = new Machine();

            machine.Push(6);
            machine.Push(3);

            expression.Evaluate(machine);

            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }
    }
}
