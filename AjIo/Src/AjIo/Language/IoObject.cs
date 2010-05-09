namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Methods;
    using AjIo.Methods.Comparison;

    public class IoObject : BaseObject
    {
        public override IObject Parent { get { return null; } }

        public IoObject()
        {
            this.SetSlot("clone", new CloneMethod());
            this.SetSlot("setSlot", new SetSlotMethod());
            this.SetSlot("newSlot", new NewSlotMethod());
            this.SetSlot("updateSlot", new UpdateSlotMethod());
            this.SetSlot(":=", new SetSlotMethod());
            this.SetSlot("::=", new NewSlotMethod());
            this.SetSlot("=", new UpdateSlotMethod());
            this.SetSlot("method", new MethodMethod());
            this.SetSlot("block", new MethodMethod());
            this.SetSlot("==", new EqualsMethod());
            this.SetSlot("!=", new NotEqualsMethod());
            this.SetSlot("if", new IfMethod());
            this.SetSlot("list", new ListMethod());
        }

        public override string TypeName { get { return "Object"; } }
    }
}
