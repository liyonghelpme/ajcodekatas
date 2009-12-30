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
        public void CreateAndRunTask()
        {
            int y = 0;

            Task<int> task = new Task<int>(x => { y = x; }, 2);

            task.Run();

            Assert.AreEqual(2, y);
        } 
    }
}
