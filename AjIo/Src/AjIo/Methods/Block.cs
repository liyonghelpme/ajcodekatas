namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjIo.Language;

    public class Block : BaseMethod
    {
        private IObject context;
        private IList<string> argumentNames;
        private IMessage body;

        public Block(IObject context, IMessage body, IList<string> argumentNames)
        {
            this.context = context;
            this.body = body;
            this.argumentNames = argumentNames;
        }

        public IList<string> ArgumentNames { get { return this.argumentNames; } }

        public IMessage Body { get { return this.body; } }

        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            LocalObject local = new LocalObject(receiver);

            for (int k = 0; this.argumentNames != null && k < this.argumentNames.Count; k++)
                local.SetLocalSlot(this.argumentNames[k], arguments[k]);

            local.SetLocalSlot("self", receiver);

            return this.body.Send(this.context, local);
        }
    }
}
