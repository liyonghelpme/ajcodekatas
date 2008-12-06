using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace AjProlog.Core
{
    public class FactNode : Node
    {
        private PrologMachineStatus mStatus;
        private IEnumerator mFactsEnum;

        public FactNode(PrologObject obj, PrologMachine mach)
            : base(mach,obj)
        {
        }

        public override bool ExecuteCall()
        {
            mFactsEnum = Machine.GetFacts(this.Object).GetEnumerator();
            return NextFact();
        }

        bool NextFact()
        {
            if (mFactsEnum == null)
            {
                return false;
            }

            while (mFactsEnum.MoveNext())
            {
                PrologObject fact = (PrologObject) mFactsEnum.Current;
                SaveStatus();
                fact = Machine.AdjustVariables(fact);
                if (fact is StructureObject && ((StructureObject)(fact)).Functor.Equals(IfPrimitive.GetInstance()))
                {
                    StructureObject ifpo;
                    ifpo = ((StructureObject)(fact));
                    PrologObject thenpo = ifpo.Parameters[1];
                    fact = ifpo.Parameters[0];
                    if (Machine.Unify(this.Object, fact))
                    {
                        Machine.Level += 1;
                        Machine.PushPending(thenpo);
                        Machine.PushNode(this);
                        return true;
                    }
                }
                else
                {
                    if (Machine.Unify(this.Object, fact))
                    {
                        Machine.PushNode(this);
                        return true;
                    }
                }
                RestoreStatus();
            }
            Machine.PushPending(this);
            return false;
        }

        protected void SaveStatus()
        {
            mStatus = Machine.Status;
        }

        protected void RestoreStatus()
        {
            Machine.Status = mStatus;
        }

        public override bool ExecuteRedo()
        {
            RestoreStatus();
            return NextFact();
        }

        public void DoCut()
        {
            mFactsEnum = null;
        }
    }
}
