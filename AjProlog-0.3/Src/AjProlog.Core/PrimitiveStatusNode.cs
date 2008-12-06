using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class PrimitiveStatusNode : PrimitiveNode
    {
        private PrologMachineStatus status;

	    public PrimitiveStatusNode(PrologMachine pm, StructureObject st)
            : base(pm, st)
	    {
	    }

	    public PrimitiveStatusNode(PrologMachine pm, Primitive obj)
            : base(pm, obj)
	    {
	    }

	    public override bool ExecuteCall()
	    {
            SaveStatus();

            if (base.ExecuteCall())
                return true;

            RestoreStatus();

            return false;
	    }

	    public override bool ExecuteRedo()
	    {
            RestoreStatus();
		    Machine.PushPending(this);
		    return false;
	    }

        private void SaveStatus()
        {
            this.status = Machine.Status;
        }

        private void RestoreStatus()
        {
            Machine.Status = this.status;
        }
    }
}

