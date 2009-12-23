using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjClipper.Language;
using AjClipper.Commands;
using AjClipper.Expressions;

namespace AjClipper.Tests
{
    [TestClass]
    public class ProcedureTests
    {
        [TestMethod]
        public void EvaluateSimpleProcedure()
        {
            Procedure procedure = new Procedure("foo", new string[] { "x" }, new ReturnCommand(new NameExpression("x")), new Machine());

            object result = procedure.Apply(new object[] { 1 }, null);

            Assert.AreEqual(1, result);
        }
    }
}
