using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class Variable : SimpleObject
    {
        private int mId;
        private PrologObject mValue;
        private PrologMachine mMachine;

        public Variable(int id, PrologMachine mach)
        {
            mId = id;
            mMachine = mach;
        }

        public int Id
        {
            get
            {
                return mId;
            }
        }

        public void Bind(PrologObject obj)
        {
            if (obj is Variable)
            {
                Variable v = ((Variable)(obj));
                if (v.Id > mId)
                {
                    v.Bind(this);
                    return;
                }
            }
            mValue = obj;
            mMachine.Bindings.Add(this);
        }

        public void Unbind()
        {
            mValue = null;
        }

        public override PrologObject Dereference()
        {
            if (mValue == null | mValue == this)
            {
                return this;
            }
            return mValue.Dereference;
        }

        public override int GetHashCode()
        {
            return mId.GetHashCode + mMachine.GetHashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj.GetType() == this.GetType))
            {
                return false;
            }
            return mId == ((Variable)(obj)).mId && mMachine.Equals(((Variable)(obj)).mMachine);
        }

        public override string ToString()
        {
            PrologObject obj;
            obj = Dereference();
            if (!(obj == this))
            {
                return obj.ToString;
            }
            return "_" + mId;
        }

        public override PrologObject Evaluate(PrologMachine pm)
        {
            return Dereference();
        }
    }
}
