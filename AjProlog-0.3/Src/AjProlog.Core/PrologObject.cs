using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public abstract class PrologObject
    {

        public virtual PrologObject Dereference()
        {
            return this;
        }

        public virtual bool Unify(PrologObject po)
        {
            PrologObject po0;
            po0 = this.Dereference();
            if (!(po0 == this))
            {
                return po0.Unify(po);
            }
            po = po.Dereference();
            if (po is Variable)
            {
                return po.Unify(this);
            }
            return Equals(po);
        }

        public virtual Node MakeNode(PrologMachine pm)
        {
            return new FactNode(this, pm);
        }

        public virtual string ToDisplayString()
        {
            return ToString();
        }

        public virtual object ToObject()
        {
            return ToString();
        }

        public virtual PrologObject Evaluate(PrologMachine pm)
        {
            return this;
        }
    }
}
