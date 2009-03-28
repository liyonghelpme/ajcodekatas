namespace AjProcessor.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    internal class PostThread
    {
        private IProcessor processor;

        internal PostThread(IProcessor processor)
        {
            this.processor = processor;
        }

        internal void PostMessage(Message message)
        {
            (new Thread(this.Process)).Start(message);
        }

        internal void Process(object message)
        {
            this.processor.ProcessMessage((Message)message);
        }
    }
}
