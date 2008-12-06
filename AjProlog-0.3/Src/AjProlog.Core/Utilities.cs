using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    class Utilities
    {

	    static PrologObject[] Evaluate(PrologObject[] pars)
	    {
		    if (pars == null) {
			    return null;
		    }
		    if (pars.Length == 0) {
			    return null;
		    }
		    PrologObject[] result;
		    result = new PrologObject[pars.Length];
		    int np;
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
		    if (char.IsUpper(name.Chars(0))) {
			    return true;
		    }
		    if (name.Chars(0) == '_') {
			    return true;
		    }
		    return false;
	    }

	    static PrologObject ToPrologObject(object obj)
	    {
		    if (obj is string) {
			    string txt = ((string)(obj));
			    PrologObject po;
			    po = Primitives.GetPrimitive(obj);
			    if (po == null) {
				    return new StringObject(obj);
			    }
			    return po;
		    }
		    if (obj is int) {
			    return new IntegerObject(((int)(obj)));
		    }
		    if (obj is PrologObject) {
			    return ((PrologObject)(obj));
		    }
		    if (obj == null) {
			    return null;
		    }
		    throw new ArgumentException("Objecto ilegal " + obj.ToString);
	    }

	    static bool IsAtom(PrologObject po)
	    {
		    if (po is StringObject) {
			    return true;
		    }
		    return false;
	    }

	    static bool IsInteger(PrologObject po)
	    {
		    if (po is IntegerObject) {
			    return true;
		    }
		    return false;
	    }

	    static bool IsAtomic(PrologObject po)
	    {
		    if (IsAtom(po) || IsInteger(po)) {
			    return true;
		    }
		    return false;
	    }

	    static bool IsVariable(PrologObject po)
	    {
		    if (po is Variable) {
			    return true;
		    }
		    return false;
	    }
    }
}
