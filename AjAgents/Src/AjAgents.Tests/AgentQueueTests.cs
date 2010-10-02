using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace AjAgents.Tests
{
    [TestClass]
    public class AgentQueueTests
    {
        [TestMethod]
        public void CreateAndUseAgentQueue()
        {
            AgentQueue<Counter> queue = new AgentQueue<Counter>(1);

            Thread thread = new Thread(new ThreadStart(delegate() { queue.Enqueue(c => { c.Increment(); }); }));
            thread.Start();

            Action<Counter> action = queue.Dequeue();
            Assert.IsNotNull(action);

            Counter counter = new Counter();
            action(counter);
            Assert.AreEqual(1, counter.Count);
        }

        [TestMethod]
        public void CreateAndUseAgentQueueTenTimes()
        {
            AgentQueue<Counter> queue = new AgentQueue<Counter>(5);

            Thread thread = new Thread(new ThreadStart(delegate() { for (int k=1; k<=10; k++) queue.Enqueue(c => { c.Increment(); }); }));
            thread.Start();

            Counter counter = new Counter();

            for (int j = 1; j <= 10; j++)
            {
                Action<Counter> action = queue.Dequeue();
                Assert.IsNotNull(action);
                action(counter);
                Assert.AreEqual(j, counter.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfZeroInConstructor()
        {
            new AgentQueue<Counter>(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNegativeNumberInConstructor()
        {
            new AgentQueue<Counter>(-1);
        }
    }
}
