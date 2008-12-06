namespace AjProlog.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AndNode : PrimitiveNode
    {
        public AndNode(PrologMachine pm, StructureObject st)
            : base(pm, st)
        {
        }

        public AndNode(PrologMachine pm, Primitive obj)
            : base(pm, obj)
        {
        }

        public override bool IsPushable()
        {
            return false;
        }
    }
}
