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
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfApplyParameterToConstantFunction()
        {
            (new ConstantFunction(0)).Apply(new ConstantFunction(1));
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

        [TestMethod]
        public void BindFirstParameter()
        {
            ParameterFunction parameter = new ParameterFunction(0, 0);

            IFunction result = parameter.Bind(new IFunction[] { new ConstantFunction(0) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod]
        public void BindSecondParameter()
        {
            ParameterFunction parameter = new ParameterFunction(1, 0);

            IFunction result = parameter.Bind(new IFunction[] { new ConstantFunction(0), new ConstantFunction(1) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(1, result.Value);
        }

        [TestMethod]
        public void BindThirdFreeParameter()
        {
            ParameterFunction parameter = new ParameterFunction(2, 0);

            IFunction result = parameter.Bind(new IFunction[] { new ConstantFunction(0), new ConstantFunction(1) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ParameterFunction));

            Assert.AreEqual(0, ((ParameterFunction)result).Position);
        }

        [TestMethod]
        public void AddTwoIntegersUsingParameters()
        {
            IFunction add = new CombineFunction(new CombineFunction(new AddIntegerFunction(), new ParameterFunction(0,0)), new ParameterFunction(1,0));
            IFunction result = add.Bind(new IFunction[] { new ConstantFunction(1), new ConstantFunction(2) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void ComposeTwoFunctionalParametersUsingBindApply()
        {
            IFunction composed = new ComposeFunction(new ParameterFunction(0,1), new ParameterFunction(1,1));

            IFunction bound = composed.Bind(new IFunction[] { new IncrementFunction(), new IncrementFunction() });

            IFunction result = bound.Apply(new ConstantFunction(1));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void ComposeTwoFunctionalParametersUsingApplyBind()
        {
            IFunction composed = new ComposeFunction(new ParameterFunction(0, 1), new ParameterFunction(1, 1));

            IFunction applied = composed.Apply(new ConstantFunction(1));

            IFunction result = applied.Bind(new IFunction[] { new IncrementFunction(), new IncrementFunction() });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public void ComposeAddWithIncrement()
        {
            IFunction composed = new ComposeFunction(new AddIntegerFunction(), new IncrementFunction());

            IFunction result = composed.Evaluate(new IFunction[] { new ConstantFunction(1), new ConstantFunction(2) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(4, result.Value);
        }

        [TestMethod]
        public void ComposeAddWithAdd()
        {
            IFunction composed = new ComposeFunction(new AddIntegerFunction(), new AddIntegerFunction());

            IFunction result = composed.Evaluate(new IFunction[] { new ConstantFunction(1), new ConstantFunction(2), new ConstantFunction(3) });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantFunction));
            Assert.AreEqual(6, result.Value);
        }
    }
}
