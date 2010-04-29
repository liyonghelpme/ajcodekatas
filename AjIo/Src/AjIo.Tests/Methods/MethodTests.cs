using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjIo.Methods;

namespace AjIo.Tests.Methods
{
    [TestClass]
    public class MethodTests
    {
        [TestMethod]
        public void NewMethodCreateInstance()
        {
            NewMethod method = new NewMethod();
            object result = method.Execute(new Machine(), typeof(System.IO.DirectoryInfo), new object[] { "." });
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(System.IO.DirectoryInfo));
        }
    }
}
