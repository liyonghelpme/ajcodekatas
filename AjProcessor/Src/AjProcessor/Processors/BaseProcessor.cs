namespace AjProcessor.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseProcessor : IProcessor
    {
        public event MessageHandler ForwardMessage;

        public virtual void ProcessMessage(Message message)
        {
            this.PostMessage(message);
        }

        public virtual void PostMessage(Message message)
        {
            if (this.ForwardMessage != null)
                this.ForwardMessage(message);
        }
    }
}
