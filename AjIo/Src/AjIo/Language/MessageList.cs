namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MessageList : IMessage
    {
        private IList<IMessage> messages = new List<IMessage>();

        public MessageList(IMessage first)
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
            object result = receiver;

            foreach (IMessage message in this.messages)
                result = message.Send(context, receiver);

            return result;
        }
    }
}
