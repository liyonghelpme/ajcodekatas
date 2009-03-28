namespace AjProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public delegate void MessageHandler(Message message);

    public interface IProcessor
    {
        void ProcessMessage(Message message);
        void PostMessage(Message message);
        void RegisterProcessor(IProcessor processor);
    }
}
