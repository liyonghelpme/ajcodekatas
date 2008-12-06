using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
public class PrimitiveNode : Node
{
	private Primitive mPrimitive;
	private PrologObject[] mParameters;

	public PrimitiveNode(PrologMachine pm, StructureObject st)
        : base(pm, st)
	{
		mPrimitive = ((Primitive)(st.Functor));
		mParameters = st.Parameters;
	}

	public PrimitiveNode(PrologMachine pm, Primitive obj)
        : base(pm, obj)
	{
		mPrimitive = obj;
		mParameters = null;
	}

	public override bool ExecuteCall()
	{
		if (mPrimitive.Execute(Machine, Evaluate(mParameters))) {
			Machine.PushNode(this);
			return true;
		}
		Machine.PushPending(this);
		return false;
	}

	public override bool ExecuteRedo()
	{
		Machine.PushPending(this);
		return false;
	}
}
}
