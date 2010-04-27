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

        public object Send(IObject context, IObject receiver)
        {
            return this.obj;
        }
    }
}
