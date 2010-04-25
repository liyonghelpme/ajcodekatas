namespace AjIo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class Machine : ClonedObject
    {
        public Machine()
            : base(new IoObject())
        {
            this.SetSlot("Object", this.Parent);
        }

        public object Process(object receiver, Message message)
        {
            IObject iob = receiver as IObject;

            if (iob != null)
                return iob.Process(this, message);

            throw new NotImplementedException();
        }
    }
}
