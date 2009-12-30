using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AjConcurr.Tests
{
    [TestClass]
    public class GoRoutinesTests
    {
        [TestMethod]
        public void RunGoRoutine()
        {
            int i = 0;
            GoRoutines.Go(delegate() { i++;  });

            Thread.Sleep(100);

            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void RunGoRoutineWithParameter()
        {
            int i = 0;
            GoRoutines.Go(x => i = x, 2);

            Thread.Sleep(100);

            Assert.AreEqual(2, i);
        }
    }
}
