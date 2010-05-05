namespace AjIo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;
    using AjIo.Methods.Arithmetic;

    public class Machine : ClonedObject
    {
        public Machine()
            : base(new IoObject())
        {
            this.SetSlot("Object", this.Parent);
            this.SetSlot("List", new ListObject(this.Parent));
        }
    }
}
