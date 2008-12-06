using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
public class PrimitiveNode : Node
{
	private Primitive primitive;
	private PrologObject[] parameters;

	public PrimitiveNode(PrologMachine pm, StructureObject st)
        : base(pm, st)
	{
		primitive = ((Primitive)(st.Functor));
		parameters = st.Parameters;
	}

	public PrimitiveNode(PrologMachine pm, Primitive obj)
        : base(pm, obj)
	{
		primitive = obj;
		parameters = null;
	}

	public override bool ExecuteCall()
	{
		if (primitive.Execute(Machine, Utilities.Evaluate(parameters))) {
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
