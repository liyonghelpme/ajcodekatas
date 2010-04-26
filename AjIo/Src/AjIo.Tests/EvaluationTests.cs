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

        [TestMethod]
        public void EvaluateDefineDog()
        {
            this.Evaluate("setSlot(\"Dog\", Object clone)");

            object result = this.machine.GetSlot("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClonedObject));
        }

        [TestMethod]
        public void EvaluateDefineDogAndSetName()
        {
            this.Evaluate("setSlot(\"Dog\", Object clone)");
            this.Evaluate("Dog setSlot(\"name\", \"Fido\")");

            object result = this.machine.GetSlot("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IObject));

            result = ((IObject)result).GetSlot("name");

            Assert.IsNotNull(result);
            Assert.AreEqual("Fido", result);
        }

        [TestMethod]
        public void EvaluateDefineDogWithMethod()
        {
            this.Evaluate("setSlot(\"Dog\", Object clone)");
            this.Evaluate("Dog setSlot(\"name\", \"Fido\")");
            this.Evaluate("Dog setSlot(\"getName\", method(name))");

            object result = this.Evaluate("Dog getName");

            Assert.IsNotNull(result);
            Assert.AreEqual("Fido", result);
        }

        [TestMethod]
        public void EvaluateDefineDogUsingAssigment()
        {
            this.Evaluate("Dog := Object clone");

            object result = this.machine.GetSlot("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClonedObject));
        }

        [TestMethod]
        public void EvaluateDefineDogUsingAssigmentWithExpression()
        {
            this.Evaluate("Dog ::= Object clone clone");

            object result = this.machine.GetSlot("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClonedObject));
        }

        [TestMethod]
        public void EvaluateDefineDogAndSetSlot()
        {
            this.Evaluate("Dog ::= Object clone clone");
            this.Evaluate("Dog name ::= \"Fido\"");

            object result = this.machine.GetSlot("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClonedObject));

            Assert.AreEqual("Fido", ((IObject)result).GetSlot("name"));
        }

        [TestMethod]
        public void EvaluateManyDefineDogAndSetSlot()
        {
            this.EvaluateMany("Dog ::= Object clone clone; Dog name ::= \"Fido\"");

            object result = this.machine.GetSlot("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ClonedObject));

            Assert.AreEqual("Fido", ((IObject)result).GetSlot("name"));
        }

        private object Evaluate(string text)
        {
            Parser parser = new Parser(text);

            object expression = parser.ParseExpression();

            Assert.IsNull(parser.ParseExpression());

            return machine.Evaluate(expression);
        }

        private void EvaluateMany(string text)
        {
            Parser parser = new Parser(text);

            for (object expression = parser.ParseExpression(); expression != null; expression = parser.ParseExpression())
                this.machine.Evaluate(expression);
        }
    }
}
