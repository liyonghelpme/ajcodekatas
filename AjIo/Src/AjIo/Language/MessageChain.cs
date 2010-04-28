namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MessageChain : IMessage
    {
        private IList<IMessage> messages = new List<IMessage>();

        public MessageChain(IMessage first)
        {
            this.messages.Add(first);
        }

        public IList<IMessage> Messages { get { return this.messages; } }

        public void AddMessage(IMessage message)
        {
            this.messages.Add(message);
        }

        public object Send(IObject context, object receiver)
        {
            foreach (IMessage message in this.messages)
                receiver = message.Send(context, receiver);

            return receiver;
        }
    }
}
