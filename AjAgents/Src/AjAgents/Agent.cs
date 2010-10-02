namespace AjAgents
{
    using System;
    using System.Threading;

    public class Agent<T> : IAgent<T>
    {
        private T instance;
        private AgentQueue<T> queue;
        private bool running;

        public Agent(T instance)
        {
            this.instance = instance;
        }

        public void Post(Action<T> action)
        {
            if (!this.running)
                this.Start();

            this.queue.Enqueue(action);
        }

        private void Start()
        {
            lock (this)
            {
                if (this.running)
                    return;

                this.queue = new AgentQueue<T>();

                Thread thread = new Thread(new ThreadStart(this.Execute));
                thread.IsBackground = true;
                thread.Start();

                this.running = true;
            }
        }

        private void Execute()
        {
            while (true)
            {
                try
                {
                    Action<T> action = this.queue.Dequeue();
                    action(this.instance);
                }
                catch (Exception ex)
                {
                    // TODO review output, maybe raise an event
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
