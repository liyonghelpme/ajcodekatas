namespace AjProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public delegate void MessageHandler(Message message);

    public interface IProcessor
    {
        event MessageHandler ForwardMessage;
        void ProcessMessage(Message message);
    }
}
