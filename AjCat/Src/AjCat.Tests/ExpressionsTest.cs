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
