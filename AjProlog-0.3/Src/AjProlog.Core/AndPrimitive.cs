using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class AndPrimitive : Primitive
    {
	    private static AndPrimitive mInstance = new AndPrimitive();
	    public const string Value = ",";

	    private AndPrimitive()
            : base(Value)
	    {
	    }

	    public static AndPrimitive GetInstance()
	    {
		    return mInstance;
	    }

	    private void Add(PrologMachine pm, PrologObject po)
	    {
		    if (po is StructureObject && ((StructureObject)(po)).Functor == this) {
			    StructureObject ast = ((StructureObject)(po));
			    Add(pm, ast.Parameters[1]);
			    Add(pm, ast.Parameters[0]);
		    } else {
			    pm.PushPending(po);
		    }
	    }

	    public override bool Execute(PrologMachine pm, params PrologObject[] pars)
	    {
		    if (pars == null || pars.Length != 2) {
			    throw new ArgumentException("AndPrimitive expects two arguments");
		    }
		    Add(pm, pars[1]);
		    Add(pm, pars[0]);
		    return true;
	    }

	    public override Node MakeNode(PrologMachine pm)
	    {
		    return new AndNode(pm, this);
	    }
    }
}
