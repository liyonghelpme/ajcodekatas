using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public abstract class Node
    {
        private PrologMachine mMachine;
        private PrologObject mObject;

        public Node(PrologMachine mach, PrologObject obj)
        {
            mMachine = mach;
            mObject = obj;
        }

        public PrologMachine Machine
        {
            get
            {
                return mMachine;
            }
        }

        public PrologObject Object
        {
            get
            {
                return mObject;
            }
        }

        public virtual bool IsPushable()
        {
            return true;
        }

        public virtual void ExecutePush()
        {
        }

        public abstract bool ExecuteCall();

        public abstract bool ExecuteRedo();
    }
}
