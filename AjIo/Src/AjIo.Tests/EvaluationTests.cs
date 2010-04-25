using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjIo.Compiler;
using AjIo.Language;

namespace AjIo.Tests
{
    [TestClass]
    public class EvaluationTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void EvaluateInteger()
        {
            object result = this.Evaluate("12");
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void EvaluateIdentifier()
        {
            this.machine.SetSlot("foo", "bar");
            object result = this.Evaluate("foo");
            Assert.AreEqual("bar", result);
        }

        [TestMethod]
        public void EvaluateObject()
        {
            object result = this.Evaluate("Object");
            Assert.IsInstanceOfType(result, typeof(IoObject));
        }

        [TestMethod]
        public void EvaluateObjectClone()
        {
            object result = this.Evaluate("Object clone");
            Assert.IsInstanceOfType(result, typeof(ClonedObject));
        }

        private object Evaluate(string text)
        {
            Parser parser = new Parser(text);

            object expression = parser.ParseExpression();

            return machine.Evaluate(expression);
        }
    }
}
