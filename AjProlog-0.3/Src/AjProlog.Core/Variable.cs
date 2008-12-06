using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class Variable : SimpleObject
    {
        private int id;
        private PrologObject value;
        private PrologMachine machine;

        public Variable(int id, PrologMachine mach)
        {
            this.id = id;
            this.machine = mach;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public void Bind(PrologObject obj)
        {
            if (obj is Variable)
            {
                Variable v = ((Variable)(obj));
                if (v.Id > id)
                {
                    v.Bind(this);
                    return;
                }
            }

            this.value = obj;
            this.machine.Bindings.Add(this);
        }

        public void Unbind()
        {
            value = null;
        }

        public override PrologObject Dereference()
        {
            if (this.value == null || this.value == this)
            {
                return this;
            }

            return value.Dereference();
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode() + this.machine.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj.GetType().Equals(this.GetType())))
            {
                return false;
            }
            return this.id == ((Variable)(obj)).id && machine.Equals(((Variable)(obj)).machine);
        }

        public override string ToString()
        {
            PrologObject obj;
            obj = Dereference();
            if (!(obj == this))
            {
                return obj.ToString();
            }
            return "_" + this.id;
        }

        public override PrologObject Evaluate(PrologMachine pm)
        {
            return this.Dereference();
        }
    }
}
