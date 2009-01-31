namespace AjLambda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLambda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LambdaTests
    {
        [TestMethod]
        public void ShouldCreateWithSimpleBody()
        {
            Variable variable = new Variable("x");
            Lambda lambda = new Lambda(variable, variable);

            Assert.IsNotNull(lambda);
            Assert.AreEqual("\\x.x", lambda.ToString());
        }
    }
}
