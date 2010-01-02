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
            AutoResetEvent handle = new AutoResetEvent(false);

            GoRoutines.Go(delegate() { i++; handle.Set(); });

            handle.WaitOne();

            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void RunGoRoutineWithParameter()
        {
            int i = 0;
            AutoResetEvent handle = new AutoResetEvent(false);

            GoRoutines.Go(x => { i = x; handle.Set(); }, 2);

            handle.WaitOne();

            Assert.AreEqual(2, i);
        }

        [TestMethod]
        public void RunGoRoutineWithTwoParameters()
        {
            int i = 0;
            AutoResetEvent handle = new AutoResetEvent(false);

            GoRoutines.Go((x, y) => { i = x + y; handle.Set(); }, 2, 3);

            handle.WaitOne();

            Assert.AreEqual(5, i);
        }

        [TestMethod]
        public void RunGoRoutineWithThreeParameters()
        {
            int i = 0;
            GoRoutines.Go((x, y, z) => i = x + y + z, 2, 3, 4);

            Thread.Sleep(100);

            Assert.AreEqual(9, i);
        }
    }
}
