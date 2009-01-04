namespace AjCat.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjCat.Compiler;
    using AjCat.Expressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionsTest
    {
        [TestMethod]
        public void RetrievesIntegerExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["add_int"] = typeof(IntegerAddOperation);
            types["sub_int"] = typeof(IntegerSubtractOperation);
            types["mult_int"] = typeof(IntegerMultiplyOperation);
            types["div_int"] = typeof(IntegerDivideOperation);
            types["inc"] = typeof(IntegerIncrementOperation);
            types["dec"] = typeof(IntegerDecrementOperation);
            types["mod_int"] = typeof(IntegerModuleOperation);
            types["lt_int"] = typeof(IntegerLessThanExpression);
            types["lteq_int"] = typeof(IntegerLessEqualThanExpression);
            types["gt_int"] = typeof(IntegerGreaterThanExpression);
            types["gteq_int"] = typeof(IntegerGreaterEqualThanExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesStackExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["#clr"] = typeof(ClearExpression);
            types["pop"] = typeof(PopExpression);
            types["dup"] = typeof(DupExpression);
            types["swap"] = typeof(SwapExpression);
            types["id"] = typeof(IdExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesListExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["nil"] = typeof(NilExpression);
            types["list"] = typeof(ListExpression);
            types["cons"] = typeof(ConsExpression);
            types["uncons"] = typeof(UnconsExpression);
            types["empty"] = typeof(EmptyExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesControlExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["if"] = typeof(IfExpression);
            types["while"] = typeof(WhileExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesBooleanExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["not"] = typeof(NotExpression);
            types["and"] = typeof(AndExpression);
            types["or"] = typeof(OrExpression);
            types["false"] = typeof(FalseExpression);
            types["true"] = typeof(TrueExpression);
            types["eq"] = typeof(EqualsExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesFunctionalExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["#load"] = typeof(LoadExpression);
            types["eval"] = typeof(EvalExpression);
            types["papply"] = typeof(PartialApplyExpression);
            types["apply"] = typeof(ApplyExpression);
            types["compose"] = typeof(ComposeExpression);
            types["quote"] = typeof(QuoteExpression);
            types["dip"] = typeof(DipExpression);

            this.GetTypes(types);
        }

        private void GetTypes(Dictionary<string, Type> types) 
        {
            foreach (string name in types.Keys) 
            {
                Expression expression = this.GetByName(name);
                Assert.IsInstanceOfType(expression, types[name]);
                Assert.AreEqual(name, expression.ToString());
            }
        }

        [TestMethod]
        public void DefineNewExpression()
        {
            Expression expression = new StringExpression("bar");
            Expressions.DefineExpression("foo", expression);

            Assert.IsNotNull(Expressions.GetByName("foo"));
            Assert.AreEqual(expression, Expressions.GetByName("foo"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfNameIsNullInDefine()
        {
            Expressions.DefineExpression(null, new StringExpression("foo"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RaiseIfExpressionIsNullInDefine()
        {
            Expressions.DefineExpression("foo", null);
        }

        [TestMethod]
        public void RedefineExpression()
        {
            Expression expression = new StringExpression("bar");
            Expressions.DefineExpression("foo", expression);
            Assert.IsNotNull(Expressions.GetByName("foo"));

            Expression newexpression = new StringExpression("foo");

            Expressions.DefineExpression("foo", newexpression);

            Assert.AreEqual(newexpression, Expressions.GetByName("foo"));
        }

        private Expression GetByName(string name)
        {
            return Expressions.GetByName(name);
        }
    }
}
