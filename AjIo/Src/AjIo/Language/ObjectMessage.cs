namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ObjectMessage : IMessage
    {
        private object obj;

        public ObjectMessage(object obj)
        {
            this.obj = obj;
        }

        public object Object { get { return this.obj; } }

        public object Send(IObject context, IObject receiver)
        {
            return this.obj;
        }

        public object Send(IObject context, object receiver)
        {
            return this.obj;
        }
    }
}
