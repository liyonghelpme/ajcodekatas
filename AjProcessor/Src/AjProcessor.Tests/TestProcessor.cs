using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjProcessor.Processors;

namespace AjProcessor.Tests
{
    internal class TestProcessor : BaseProcessor
    {
        public Message ProcessedMessage { get; set; }

        public override void ProcessMessage(Message message)
        {
            this.ProcessedMessage = message;
            base.ProcessMessage(message);
        }
    }
}
