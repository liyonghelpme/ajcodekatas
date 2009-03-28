namespace AjProcessor.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using AjProcessor.Utilities;

    public class BaseProcessor : IProcessor
    {
        private List<IProcessor> processors = new List<IProcessor>();

        public virtual void ProcessMessage(Message message)
        {
            this.PostMessage(message);
        }

        public virtual void PostMessage(Message message)
        {
            foreach (IProcessor processor in this.processors)
            {
                (new PostThread(processor)).PostMessage(message);
            }
        }

        public virtual void RegisterProcessor(IProcessor processor)
        {
            this.processors.Add(processor);
        }
    }
}
