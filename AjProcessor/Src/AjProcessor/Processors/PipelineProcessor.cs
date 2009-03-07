namespace AjProcessor.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PipelineProcessor : BaseProcessor
    {
        private bool initialized;

        public ICollection<IProcessor> Processors { get; set; }

        public override void ProcessMessage(Message message)
        {
            if (!this.initialized)
                this.Initialize();

            this.Processors.First().ProcessMessage(message);
        }

        private void Initialize()
        {
            IProcessor lastProcessor = null;

            foreach (IProcessor processor in this.Processors)
            {
                if (lastProcessor != null)
                    lastProcessor.ForwardMessage += processor.ProcessMessage;

                lastProcessor = processor;
            }

            if (lastProcessor != null)
                lastProcessor.ForwardMessage += this.PostMessage;

            this.initialized = true;
        }
    }
}
