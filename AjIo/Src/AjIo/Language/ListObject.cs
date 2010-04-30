namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Methods;

    public class ListObject : ClonedObject
    {
        public ListObject(IObject parent)
            : base(parent)
        {
            this.SetSlot("at", new AtMethod());
        }
    }

    internal class AtMethod : BaseMethod
    {
        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
