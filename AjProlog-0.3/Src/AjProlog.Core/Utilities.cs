using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class Utilities
    {

	    public static PrologObject[] Evaluate(PrologObject[] pars)
	    {
		    if (pars == null) {
			    return null;
		    }

		    if (pars.Length == 0) {
			    return null;
		    }

		    PrologObject[] result;

		    result = new PrologObject[pars.Length];

		    for (int np = 0; np <= pars.Length - 1; np++) {
			    result[np] = pars[np].Dereference();
		    }

		    return result;
	    }

	    public static bool IsVariableName(string name)
	    {
		    if (name == null || name.Length == 0) {
			    return false;
		    }

		    if (char.IsUpper(name[0])) {
			    return true;
		    }

		    if (name[0] == '_') {
			    return true;
		    }

		    return false;
	    }

	    public static PrologObject ToPrologObject(object obj)
	    {
		    if (obj is string) {
			    string txt = ((string)(obj));

			    PrologObject po;

			    po = Primitives.GetPrimitive(txt);

			    if (po == null) {
				    return new StringObject(txt);
			    }

			    return po;
		    }

		    if (obj is int) {
			    return new IntegerObject((int) obj);
		    }

		    if (obj is PrologObject) {
			    return (PrologObject)(obj);
		    }

		    if (obj == null) {
			    return null;
		    }

		    throw new ArgumentException("Illegal object: " + obj.ToString());
	    }

	    public static bool IsAtom(PrologObject po)
	    {
		    if (po is StringObject) {
			    return true;
		    }

		    return false;
	    }

	    public static bool IsInteger(PrologObject po)
	    {
		    if (po is IntegerObject) {
			    return true;
		    }

		    return false;
	    }

	    public static bool IsAtomic(PrologObject po)
	    {
		    if (IsAtom(po) || IsInteger(po)) {
			    return true;
		    }

		    return false;
	    }

	    public static bool IsVariable(PrologObject po)
	    {
		    if (po is Variable) {
			    return true;
		    }

		    return false;
	    }
    }
}
