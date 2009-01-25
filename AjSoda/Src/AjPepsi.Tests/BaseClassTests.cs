namespace AjPepsi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjPepsi;
    using AjSoda;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseClassTests
    {
        [TestMethod]
        public void ShouldCreateBaseClass()
        {
            IClass baseClass = new BaseClass();

            Assert.IsNotNull(baseClass);
            Assert.AreEqual(0, baseClass.InstanceSize);
            Assert.IsNotNull(baseClass.Behavior);
            Assert.AreEqual(baseClass, baseClass.Behavior);
        }

        [TestMethod]
        public void ShouldAddVariableNames()
        {
            IClass baseClass = new BaseClass();

            baseClass.AddVariable("a");
            baseClass.AddVariable("b");

            Assert.AreEqual(2, baseClass.InstanceSize);

            Assert.IsTrue(baseClass.InstanceVariableNames.Contains("a"));
            Assert.IsTrue(baseClass.InstanceVariableNames.Contains("b"));

            Assert.AreEqual(0, baseClass.GetInstanceVariableOffset("a"));
            Assert.AreEqual(1, baseClass.GetInstanceVariableOffset("b"));
        }

        [TestMethod]
        public void ShouldAddVariableNamesUsingSend()
        {
            IClass baseClass = new BaseClass();

            baseClass.Send("addVariable:", "a");
            baseClass.Send("addVariable:", "b");

            Assert.AreEqual(2, baseClass.InstanceSize);

            Assert.IsTrue(baseClass.InstanceVariableNames.Contains("a"));
            Assert.IsTrue(baseClass.InstanceVariableNames.Contains("b"));
        }

        [TestMethod]
        public void ShouldCreateInstance()
        {
            IClass baseClass = new BaseClass();

            IObject instance = baseClass.CreateInstance();

            Assert.IsNotNull(instance);
            Assert.AreEqual(baseClass, instance.Behavior);
            Assert.AreEqual(0, instance.Size);
        }

        [TestMethod]
        public void ShouldCreateInstanceUsingSend()
        {
            IClass baseClass = new BaseClass();

            object obj = baseClass.Send("basicNew");

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(IObject));

            IObject instance = (IObject)obj;
            Assert.AreEqual(baseClass, instance.Behavior);
            Assert.AreEqual(0, instance.Size);
        }

        [TestMethod]
        public void ShouldCreateInstanceWithVariables()
        {
            IClass baseClass = new BaseClass();

            baseClass.AddVariable("a");
            baseClass.AddVariable("b");

            IObject instance = baseClass.CreateInstance();

            Assert.IsNotNull(instance);
            Assert.AreEqual(baseClass, instance.Behavior);
            Assert.AreEqual(2, instance.Size);
            Assert.IsNull(instance.GetValueAt(0));
            Assert.IsNull(instance.GetValueAt(1));
        }

        [TestMethod]
        public void ShouldDefineSubclass()
        {
            IClass baseClass = new BaseClass();
            IBehavior subClass = baseClass.CreateDelegated();

            Assert.IsNotNull(subClass);
            Assert.IsInstanceOfType(subClass, typeof(IClass));
            Assert.AreEqual(subClass.Parent, baseClass);
            Assert.AreEqual(subClass.Behavior, baseClass.Behavior);
        }

        [TestMethod]
        public void ShouldDefineSubclassWithVariables()
        {
            IClass baseClass = new BaseClass();
            baseClass.AddVariable("a");

            IClass subClass = (IClass) baseClass.CreateDelegated();
            subClass.AddVariable("b");

            Assert.AreEqual(1, baseClass.InstanceSize);
            Assert.AreEqual(2, subClass.InstanceSize);
            Assert.AreEqual(0, baseClass.GetInstanceVariableOffset("a"));
            Assert.AreEqual(-1, subClass.GetInstanceVariableOffset("a"));
            Assert.AreEqual(1, subClass.GetInstanceVariableOffset("b"));
        }

        [TestMethod]
        public void ShouldCreateSubclassInstanceWithVariables()
        {
            IClass baseClass = new BaseClass();
            baseClass.AddVariable("a");

            IClass subClass = (IClass)baseClass.CreateDelegated();
            subClass.AddVariable("b");

            IObject instance = subClass.CreateInstance();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Size);
            Assert.IsNull(instance.GetValueAt(0));
            Assert.IsNull(instance.GetValueAt(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRaiseIfNameIsNullWhenAddVariable()
        {
            IClass baseClass = new BaseClass();

            baseClass.AddVariable(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseIfVariableNameAlreadyExists()
        {
            IClass baseClass = new BaseClass();

            baseClass.AddVariable("a");
            baseClass.AddVariable("a");
        }
    }
}
