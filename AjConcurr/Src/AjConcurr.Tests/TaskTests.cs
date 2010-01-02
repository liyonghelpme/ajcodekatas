using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AjConcurr.Tests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void CreateAndRunTaskWithOneParameter()
        {
            int y = 0;

            Task<int> task = new Task<int>(x => { y = x; }, 2);

            task.Run();

            Assert.AreEqual(2, y);
        }
    
        [TestMethod]
        public void CreateAndRunTaskWithTwoParameters()
        {
            int z = 0;

            Task<int, int> task = new Task<int, int>((x, y) => { z = x + y; }, 2, 3);

            task.Run();

            Assert.AreEqual(5, z);
        }

        [TestMethod]
        public void CreateAndRunTaskWithThreeParameters()
        {
            int z = 0;

            Task<int, int, int> task = new Task<int, int, int>((x, y, w) => { z = x + y + w; }, 2, 3, 4);

            task.Run();

            Assert.AreEqual(9, z);
        }
    }
}
