namespace AjAgents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class AgentQueue<T>
    {
        private Queue<Action<T>> queue = new Queue<Action<T>>();
        private int maxsize;

        public AgentQueue()
            : this(100)
        {
        }

        public AgentQueue(int maxsize)
        {
            if (maxsize <= 0)
                throw new InvalidOperationException("AgentQueue needs a positive maxsize");

            this.maxsize = maxsize;
        }

        public void Enqueue(Action<T> action)
        {
            lock (this)
            {
                while (this.queue.Count >= this.maxsize)
                    Monitor.Wait(this);

                this.queue.Enqueue(action);
                Monitor.PulseAll(this);
            }
        }

        public Action<T> Dequeue()
        {
            lock (this)
            {
                while (this.queue.Count == 0)
                    Monitor.Wait(this);

                Action<T> action = this.queue.Dequeue();
                Monitor.PulseAll(this);
                return action;
            }
        }
    }
}
