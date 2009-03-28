using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjProcessor.Processors;
using System.Threading;

namespace AjProcessor.Tests
{
    internal class TestProcessor : BaseProcessor
    {
        public Message ProcessedMessage { get; set; }
        public AutoResetEvent AutoEvent { get; set; }

        internal TestProcessor()
        {
            AutoEvent = new AutoResetEvent(false);
        }

        public override void ProcessMessage(Message message)
        {
            this.ProcessedMessage = message;
            base.ProcessMessage(message);
            AutoEvent.Set();
        }
    }
}
