using System;
using System.Text;
using System.Collections;
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

        [TestMethod]
        public void CreateAndEvaluateCompositeExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            CompositeExpression expression = new CompositeExpression(list);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Expressions);
            Assert.AreEqual(3, expression.Expressions.Count);

            Assert.AreEqual(0, machine.StackCount);
            expression.Evaluate(machine);
            Assert.AreEqual(3, machine.StackCount);

            Assert.AreEqual(3, machine.Pop());
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(1, machine.Pop());
        }

        [TestMethod]
        public void CreateAndEvaluateQuotationExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            QuotationExpression expression = new QuotationExpression(list);

            Assert.IsNotNull(expression);
            Assert.IsNotNull(expression.Expressions);
            Assert.AreEqual(3, expression.Expressions.Count);

            Assert.AreEqual(0, machine.StackCount);
            expression.Evaluate(machine);
            Assert.AreEqual(1, machine.StackCount);
            Assert.IsInstanceOfType(machine.Top(), typeof(CompositeExpression));

            CompositeExpression composite = (CompositeExpression)machine.Pop();

            Assert.IsNotNull(composite);
            Assert.AreEqual(3, composite.Expressions.Count);
        }

        [TestMethod]
        public void CreateAndEvaluateListExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            machine.Push(new CompositeExpression(list));

            Assert.AreEqual(1, machine.StackCount);

            ListExpression expression = new ListExpression();

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);

            Assert.IsInstanceOfType(machine.Top(), typeof(IList));

            IList result = (IList)machine.Pop();

            Assert.AreEqual(0, machine.StackCount);

            Assert.AreEqual(3, result.Count);

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [TestMethod]
        public void GetAndEvaluateDupExpression()
        {
            Expression expression = DupExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);

            Assert.AreEqual(2, machine.StackCount);

            expression.Evaluate(machine);

            Assert.AreEqual(3, machine.StackCount);
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(1, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateSwapExpression()
        {
            Expression expression = SwapExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);

            Assert.AreEqual(2, machine.StackCount);

            expression.Evaluate(machine);

            Assert.AreEqual(2, machine.StackCount);
            Assert.AreEqual(1, machine.Pop());
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluatePopExpression()
        {
            Expression expression = PopExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);

            Assert.AreEqual(2, machine.StackCount);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.AreEqual(1, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }
    }
}
