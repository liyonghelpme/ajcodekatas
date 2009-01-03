using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AjCat.Compiler;
using AjCat.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjCat.Tests
{
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
            types["mod"] = typeof(IntegerModuleOperation);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesStackExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["pop"] = typeof(PopExpression);
            types["dup"] = typeof(DupExpression);
            types["swap"] = typeof(SwapExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesListExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["list"] = typeof(ListExpression);
            types["cons"] = typeof(ConsExpression);
            types["uncons"] = typeof(UnconsExpression);
            types["empty"] = typeof(EmptyExpression);

            this.GetTypes(types);
        }

        [TestMethod]
        public void RetrievesBooleanExpressionsByName()
        {
            Dictionary<string, Type> types = new Dictionary<string, Type>();

            types["if"] = typeof(IfExpression);
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
                Assert.IsInstanceOfType(this.GetByName(name), types[name]);
            }
        }

        private Expression GetByName(string name)
        {
            return Compiler.Expressions.GetByName(name);
        }
    }
}
