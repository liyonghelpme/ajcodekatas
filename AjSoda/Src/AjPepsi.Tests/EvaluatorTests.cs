namespace AjPepsi.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    using AjPepsi;
    using AjPepsi.Compiler;
    using AjSoda;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EvaluatorTests
    {
        [TestMethod]
        public void ShouldCreateWithString()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "NewClass : Object()");

            Assert.IsNotNull(evaluator);
        }

        [TestMethod]
        public void ShouldCreateWithReader()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, new StringReader("NewClass : Object()"));

            Assert.IsNotNull(evaluator);
        }

        [TestMethod]
        public void ShouldCreateWithTokenizer()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, new Tokenizer("NewClass : Object()"));

            Assert.IsNotNull(evaluator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfMachineIsNull()
        {
            Evaluator evaluator = new Evaluator(null, "NewClass : Object()");
        }

        [TestMethod]
        public void ShouldEvaluateSimpleBlock()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "[newobject := Object basicNew]");

            evaluator.Evaluate();

            object newObject = machine.GetGlobalObject("newobject");

            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(AjSoda.IObject));
            Assert.AreEqual(((AjSoda.IObject)newObject).Behavior, machine.GetGlobalObject("Object"));
        }

        [TestMethod]
        public void ShouldEvaluateBlockWithCommands()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "[newobject := Object basicNew. otherobject := Object basicNew]");

            evaluator.Evaluate();

            object newObject = machine.GetGlobalObject("newobject");

            Assert.IsNotNull(newObject);

            object otherObject = machine.GetGlobalObject("otherobject");

            Assert.IsNotNull(otherObject);
        }

        [TestMethod]
        public void ShouldEvaluateAssignment()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "newObject := [^Object basicNew]");

            evaluator.Evaluate();

            object newObject = machine.GetGlobalObject("newObject");

            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(AjSoda.IObject));
            Assert.AreEqual(((AjSoda.IObject)newObject).Behavior, machine.GetGlobalObject("Object"));
        }

        [TestMethod]
        public void ShouldEvaluateDefine()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "Object new [^self basicNew]");

            evaluator.Evaluate();

            IObject obj = (IObject) machine.GetGlobalObject("Object");

            Assert.IsNotNull(obj.Send("lookup:", "new"));
        }

        [TestMethod]
        public void ShouldEvaluateDefineAndInvoke()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "Object new [^self basicNew] newObject := [^Object new]");

            evaluator.Evaluate();

            IObject obj = (IObject)machine.GetGlobalObject("newObject");

            Assert.IsNotNull(obj);
        }
    }
}

