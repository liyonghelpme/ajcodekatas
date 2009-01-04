namespace AjCat.Tests
{
    using System;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using AjCat;
    using AjCat.Compiler;
    using AjCat.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionTest
    {
        [TestMethod]
        public void CreateIntegerExpression()
        {
            IntegerExpression expression = new IntegerExpression(10);

            Assert.AreEqual(10, expression.Value);

            Assert.AreEqual("10", expression.ToString());
        }

        [TestMethod]
        public void CreateStringExpression()
        {
            StringExpression expression = new StringExpression("foo");

            Assert.AreEqual("foo", expression.Value);
            Assert.AreEqual("\"foo\"", expression.ToString());
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
        public void GetAndEvaluateIntegerIncrementExpression()
        {
            IntegerIncrementOperation expression = IntegerIncrementOperation.Instance;

            Machine machine = new Machine();

            machine.Push(1);

            expression.Evaluate(machine);

            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateIntegerDecrementExpression()
        {
            IntegerDecrementOperation expression = IntegerDecrementOperation.Instance;

            Machine machine = new Machine();

            machine.Push(1);

            expression.Evaluate(machine);

            Assert.AreEqual(0, machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateIntegerModuleExpression()
        {
            IntegerModuleOperation expression = IntegerModuleOperation.Instance;

            Machine machine = new Machine();

            machine.Push(5);
            machine.Push(4);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.Pop());
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
        public void CreateAndEvaluateDefineExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            DefineExpression expression = new DefineExpression("foo", list);

            Assert.IsNotNull(expression);
            Assert.AreEqual("foo", expression.Name);
            Assert.IsNotNull(expression.Expressions);
            Assert.AreEqual(3, expression.Expressions.Count);

            Assert.AreEqual(0, machine.StackCount);
            expression.Evaluate(machine);
            Assert.AreEqual(0, machine.StackCount);

            Assert.IsNotNull(Expressions.GetByName("foo"));
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
            Assert.AreEqual("[1 2 3]", expression.ToString());

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
        public void GetAndEvaluateListExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            machine.Push(new CompositeExpression(list));

            Assert.AreEqual(1, machine.StackCount);

            ListExpression expression = ListExpression.Instance;

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
        public void GetAndEvaluateApplyExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            machine.Push(new CompositeExpression(list));

            Assert.AreEqual(1, machine.StackCount);

            ApplyExpression expression = ApplyExpression.Instance;

            expression.Evaluate(machine);

            Assert.AreEqual(3, machine.StackCount);
            Assert.AreEqual(3, machine.Pop());
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(1, machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluateDipExpression()
        {
            Machine machine = new Machine();
            List<Expression> list = new List<Expression>();

            list.Add(new IntegerExpression(1));
            list.Add(new IntegerExpression(2));
            list.Add(new IntegerExpression(3));

            machine.Push(4);
            machine.Push(new CompositeExpression(list));

            Assert.AreEqual(2, machine.StackCount);

            DipExpression expression = DipExpression.Instance;

            expression.Evaluate(machine);

            Assert.AreEqual(4, machine.StackCount);
            Assert.AreEqual(4, machine.Pop());
            Assert.AreEqual(3, machine.Pop());
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(1, machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluateComposeExpression()
        {
            Machine machine = new Machine();
            List<Expression> list1 = new List<Expression>();

            list1.Add(new IntegerExpression(1));
            list1.Add(new IntegerExpression(2));
            list1.Add(new IntegerExpression(3));

            machine.Push(new CompositeExpression(list1));

            List<Expression> list2 = new List<Expression>();

            list2.Add(new IntegerExpression(4));
            list2.Add(new IntegerExpression(5));
            list2.Add(new IntegerExpression(6));

            machine.Push(new CompositeExpression(list2));

            Assert.AreEqual(2, machine.StackCount);

            ComposeExpression expression = ComposeExpression.Instance;

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsInstanceOfType(machine.Top(), typeof(CompositeExpression));

            ((Expression)machine.Pop()).Evaluate(machine);

            Assert.AreEqual(6, machine.StackCount);
            Assert.AreEqual(6, machine.Pop());
            Assert.AreEqual(5, machine.Pop());
            Assert.AreEqual(4, machine.Pop());
            Assert.AreEqual(3, machine.Pop());
            Assert.AreEqual(2, machine.Pop());
            Assert.AreEqual(1, machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluateNilExpression()
        {
            NilExpression expression = NilExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsInstanceOfType(machine.Top(), typeof(IList));

            IList list = (IList)machine.Pop();

            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void GetAndEvaluateConsExpression()
        {
            Machine machine = new Machine();
            IList list = new ArrayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            machine.Push(list);
            machine.Push(0);

            Assert.AreEqual(2, machine.StackCount);

            ConsExpression expression = ConsExpression.Instance;

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);

            Assert.IsInstanceOfType(machine.Top(), typeof(IList));

            IList result = (IList)machine.Pop();

            Assert.AreEqual(0, machine.StackCount);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(2, result[2]);
            Assert.AreEqual(3, result[3]);
        }

        [TestMethod]
        public void GetAndEvaluateUnconsExpression()
        {
            Machine machine = new Machine();
            IList list = new ArrayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            machine.Push(list);

            Assert.AreEqual(1, machine.StackCount);

            UnconsExpression expression = UnconsExpression.Instance;

            expression.Evaluate(machine);

            Assert.AreEqual(2, machine.StackCount);

            Assert.AreEqual(1, machine.Pop());
            Assert.IsInstanceOfType(machine.Top(), typeof(IList));

            IList result = (IList)machine.Pop();

            Assert.AreEqual(0, machine.StackCount);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3, result[1]);
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
        public void GetAndEvaluateClearExpression()
        {
            ClearExpression expression = ClearExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);
            machine.Push(3);

            Assert.AreEqual(3, machine.StackCount);
            expression.Evaluate(machine);
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

        [TestMethod]
        public void CreateAndEvaluateTrueExpression()
        {
            TrueExpression expression = new TrueExpression();

            Assert.IsNotNull(expression);
            Assert.IsTrue((bool) expression.Value);

            Machine machine = new Machine();

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool) machine.Pop());
        }

        [TestMethod]
        public void CreateAndEvaluateFalseExpression()
        {
            FalseExpression expression = new FalseExpression();

            Assert.IsNotNull(expression);
            Assert.IsFalse((bool) expression.Value);

            Machine machine = new Machine();

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool) machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluateOrExpression()
        {
            OrExpression expression = OrExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(true);
            machine.Push(true);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(true);
            machine.Push(false);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(false);
            machine.Push(true);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(false);
            machine.Push(false);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateAndExpression()
        {
            AndExpression expression = AndExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(true);
            machine.Push(true);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(true);
            machine.Push(false);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(false);
            machine.Push(true);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(false);
            machine.Push(false);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateNotExpression()
        {
            NotExpression expression = NotExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(false);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(true);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateQuoteExpression()
        {
            QuoteExpression expression = QuoteExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(2);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsInstanceOfType(machine.Top(), typeof(Expression));
            ((Expression)machine.Pop()).Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.AreEqual(2, machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluateEqualsExpression()
        {
            EqualsExpression expression = EqualsExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push(2);
            machine.Push(2);

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push("foo");
            machine.Push("bar");

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);

            machine.Push("foo");
            machine.Push("foo");

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());
            Assert.AreEqual(0, machine.StackCount);
        }

        [TestMethod]
        public void GetAndEvaluateEmptyExpression()
        {
            EmptyExpression expression = EmptyExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            IList list = new ArrayList();

            machine.Push(list);

            expression.Evaluate(machine);

            Assert.AreEqual(2, machine.StackCount);
            Assert.IsTrue((bool)machine.Pop());

            list.Add(1);
            list.Add(2);

            expression.Evaluate(machine);

            Assert.AreEqual(2, machine.StackCount);
            Assert.IsFalse((bool)machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluateIfExpression()
        {
            IfExpression expression = IfExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(true);
            machine.Push(new IntegerExpression(1));
            machine.Push(new IntegerExpression(2));

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.AreEqual(1, machine.Pop());

            machine.Push(false);
            machine.Push(new IntegerExpression(1));
            machine.Push(new IntegerExpression(2));

            expression.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.AreEqual(2, machine.Pop());
        }

        [TestMethod]
        public void GetAndEvaluatePartialApplyExpression()
        {
            PartialApplyExpression expression = PartialApplyExpression.Instance;

            Assert.IsNotNull(expression);

            Machine machine = new Machine();

            machine.Push(1);
            machine.Push(2);
            machine.Push(3);

            List<Expression> list = new List<Expression>();
            list.Add(IntegerAddOperation.Instance);
            list.Add(IntegerAddOperation.Instance);

            machine.Push(new CompositeExpression(list));

            expression.Evaluate(machine);

            Assert.AreEqual(3, machine.StackCount);
            Assert.IsInstanceOfType(machine.Top(), typeof(CompositeExpression));

            CompositeExpression result = (CompositeExpression)machine.Pop();

            Assert.AreEqual(3, result.Expressions.Count);
            Assert.IsInstanceOfType(result.Expressions[0], typeof(ConstantExpression));
            Assert.IsInstanceOfType(result.Expressions[1], typeof(IntegerAddOperation));
            Assert.IsInstanceOfType(result.Expressions[2], typeof(IntegerAddOperation));

            result.Evaluate(machine);

            Assert.AreEqual(1, machine.StackCount);
            Assert.AreEqual(6, machine.Pop());
        }
    }
}
