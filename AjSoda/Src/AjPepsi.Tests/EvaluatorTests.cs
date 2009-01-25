namespace AjPepsi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

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
            Evaluator evaluator = new Evaluator(machine, "[newobject := Object class basicNew]");

            evaluator.Evaluate();

            object newObject = machine.GetGlobalObject("newobject");

            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(AjSoda.IObject));

            IObject obj = (IObject)machine.GetGlobalObject("Object");
            Assert.AreEqual(((AjSoda.IObject)newObject).Behavior, obj.Behavior);
        }

        [TestMethod]
        public void ShouldEvaluateBlockWithCommands()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "[newobject := Object class basicNew. otherobject := Object class basicNew]");

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
            Evaluator evaluator = new Evaluator(machine, "newObject := [^Object class basicNew]");

            evaluator.Evaluate();

            object newObject = machine.GetGlobalObject("newObject");

            Assert.IsNotNull(newObject);
            Assert.IsInstanceOfType(newObject, typeof(AjSoda.IObject));

            IObject obj = (IObject) machine.GetGlobalObject("Object");
            Assert.AreEqual(((AjSoda.IObject)newObject).Behavior, obj.Behavior);
        }

        [TestMethod]
        public void ShouldCreateDelegatedPrototype()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "List : Object()");

            evaluator.Evaluate();

            IObject listObject = (IObject)machine.GetGlobalObject("List");
            IObject objObject = (IObject)machine.GetGlobalObject("Object");

            Assert.IsNotNull(listObject);
            Assert.IsNotNull(objObject);

            Assert.AreEqual(((IBehavior) listObject.Behavior).Parent, objObject.Behavior);

            Assert.AreEqual(0, listObject.Size);
        }

        [TestMethod]
        public void ShouldCreateDelegatedPrototypeWithVariables()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "List : Object(head tail)");

            evaluator.Evaluate();

            IObject listObject = (IObject)machine.GetGlobalObject("List");
            IObject objObject = (IObject)machine.GetGlobalObject("Object");

            Assert.IsNotNull(listObject);
            Assert.IsNotNull(objObject);

            Assert.AreEqual(((IBehavior)listObject.Behavior).Parent, objObject.Behavior);

            Assert.AreEqual(2, listObject.Size);
        }

        [TestMethod]
        public void ShouldEvaluateDefine()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "Object new [^self class basicNew]");

            evaluator.Evaluate();

            IObject obj = (IObject) machine.GetGlobalObject("Object");

            Assert.IsNotNull(obj.Behavior.Send("lookup:", "new"));
        }

        [TestMethod]
        public void ShouldEvaluateDefineAndInvoke()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "Object new [^self class basicNew] newObject := [^Object new]");

            evaluator.Evaluate();

            IObject obj = (IObject)machine.GetGlobalObject("newObject");
            IObject obj2 = (IObject)machine.GetGlobalObject("Object");

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj2);

            Assert.AreEqual(obj.Behavior, obj2.Behavior);
        }

        [TestMethod]
        public void ShouldEvaluateDefineAndInvokeEmptyBlock()
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, "Object initialize [] result := [^Object initialize]");

            evaluator.Evaluate();

            IObject obj = (IObject)machine.GetGlobalObject("result");
            IObject obj2 = (IObject)machine.GetGlobalObject("Object");

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj2);

            Assert.AreEqual(obj, obj2);
        }

        [TestMethod]
        [DeploymentItem(@"CodeFiles\Object01.st")]
        public void ShouldLoadObject01()
        {
            PepsiMachine machine = this.LoadFile("Object01.st");

            Assert.IsNotNull(machine.GetGlobalObject("obj1"));
            Assert.IsNotNull(machine.GetGlobalObject("Object"));

            IObject obj = (IObject)machine.GetGlobalObject("Object");
            IObject obj1 = (IObject)machine.GetGlobalObject("obj1");

            Assert.AreEqual(0, obj.Size);
            Assert.AreEqual(0, obj1.Size);

            Assert.AreEqual(obj.Behavior, obj1.Behavior);
        }

        [TestMethod]
        [DeploymentItem(@"CodeFiles\List01.st")]
        public void ShouldLoadList01()
        {
            PepsiMachine machine = this.LoadFile("List01.st");

            Assert.IsNotNull(machine.GetGlobalObject("List"));
            Assert.IsNotNull(machine.GetGlobalObject("list1"));

            IObject list = (IObject)machine.GetGlobalObject("List");
            IObject list1 = (IObject)machine.GetGlobalObject("list1");

            Assert.AreEqual(2, list.Size);
            Assert.AreEqual(2, list1.Size);

            Assert.AreEqual(list.Behavior, list1.Behavior);
        }

        [TestMethod]
        [DeploymentItem(@"CodeFiles\List02.st")]
        public void ShouldLoadList02()
        {
            PepsiMachine machine = this.LoadFile("List02.st");

            Assert.IsNotNull(machine.GetGlobalObject("list1"));
            Assert.IsNotNull(machine.GetGlobalObject("list2"));

            IObject list1 = (IObject)machine.GetGlobalObject("list1");
            IObject list2 = (IObject)machine.GetGlobalObject("list2");

            Assert.AreEqual(2, list1.Size);
            Assert.AreEqual(2, list1.Size);

            Assert.AreEqual("Hello", list1.GetValueAt(0));
            Assert.AreEqual("World", list2.GetValueAt(0));
            Assert.AreEqual(list2, list1.GetValueAt(1));
        }

        private PepsiMachine LoadFile(string fileName)
        {
            PepsiMachine machine = new PepsiMachine();
            Evaluator evaluator = new Evaluator(machine, File.ReadAllText(fileName));
            evaluator.Evaluate();

            return machine;
        }
    }
}

