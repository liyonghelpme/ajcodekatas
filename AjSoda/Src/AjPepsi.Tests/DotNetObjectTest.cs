namespace AjPepsi.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DotNetObjectTest
    {
        [TestMethod]
        public void ShouldCreateAnObject()
        {
            PepsiMachine machine = new PepsiMachine();

            object obj = DotNetObject.NewObject(Type.GetType("System.IO.FileInfo"), new object[] { "AnyFile.txt" });

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(System.IO.FileInfo));
        }

        [TestMethod]
        public void ShouldInvokeMethod()
        {
            PepsiMachine machine = new PepsiMachine();

            object obj = DotNetObject.SendMessage(new System.IO.FileInfo("NonexistentFile.txt"), "exists", null);

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(bool));
            Assert.IsFalse((bool)obj);
        }
    }
}
