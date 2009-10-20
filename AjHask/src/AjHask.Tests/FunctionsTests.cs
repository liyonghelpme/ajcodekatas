namespace AjHask.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AjHask.Language;
    using AjHask.Tests.Functions;

    [TestClass]
    public class FunctionsTests
    {
        [TestMethod]
        public void EvaluateConstantFunction()
        {
            ConstantFunction function = new ConstantFunction(1);

            Assert.AreEqual(1, function.Value);
        }

        [TestMethod]
        public void EvaluateIncrementFunctionApply() 
        {
            IFunction incr = new IncrementFunction();
            IFunction result = incr.Apply(new ConstantFunction(1));

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Arity);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));

            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void AddOneInteger()
        {
            IFunction addf = new AddIntegerFunction();
            IFunction result = addf.Apply(new ConstantFunction(1));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Arity);
            Assert.IsInstanceOfType(result, typeof(PartialFunction));
            Assert.AreEqual(result, result.Value);
        }

        [TestMethod]
        public void AddTwoIntegers()
        {
            IFunction addf = new AddIntegerFunction();
            IFunction result = addf.Apply(new ConstantFunction(1)).Apply(new ConstantFunction(2));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(0, result.Arity);

            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void RaiseIfApplyParameterToConstantFunction()
        {
            (new ConstantFunction(0)).Apply(new ConstantFunction(1));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void RaiseIfApplyParametersToConstantFunction()
        {
            (new ConstantFunction(0)).Apply(new IFunction[] { new ConstantFunction(1) });
        }

        [TestMethod]
        public void ComposeFunctions()
        {
            IFunction incr = new IncrementFunction();
            IFunction compose = new ComposeFunction(incr, incr);

            Assert.AreEqual(1, compose.Arity);
            Assert.AreEqual(compose, compose.Value);

            IFunction result = compose.Apply(new ConstantFunction(1));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(3, result.Value);
        }
    }
}
