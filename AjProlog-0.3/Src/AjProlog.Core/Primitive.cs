using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public abstract class Primitive : StringObject
    {

	    public Primitive(string n) : base(n)
	    {
	    }

	    public override Node MakeNode(PrologMachine pm)
	    {
		    return new PrimitiveNode(pm, this);
	    }

	    public virtual bool Execute(PrologMachine pm, PrologObject[][] parameters)
	    {
		    return true;
	    }

	    public virtual PrologObject Evaluate(PrologMachine pm, StructureObject st)
	    {
		    return st;
	    }

	    public virtual string ToString(PrologObject[] parameters)
	    {
		    if (parameters == null) {
			    return this.ToString();
		    }
		    if (parameters.Length == 0) {
			    return ToString();
		    }
		    StringBuilder sb = new StringBuilder(parameters.Length * 20);
		    sb.Append(ToString()).Append("(");
		    for (int np = 0; np <= parameters.Length - 1; np++) {
			    sb.Append(parameters[np].ToString());
			    if (np < parameters.Length - 1) {
				    sb.Append(",");
			    }
		    }
		    sb.Append(")");
		    return sb.ToString();
	    }
    }
}
