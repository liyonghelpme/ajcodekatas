using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace AjProlog.Core
{
    public class StructureObject : PrologObject
    {
	    private PrologObject functor;
	    private PrologObject[] parameters;

	    public StructureObject(object functor, params object[] parameters)
            : this(Utilities.ToPrologObject(functor), parameters)
	    {
	    }

	    public StructureObject(StructureObject st, PrologMachine pm, ArrayList vars, int offset)
	    {
		    this.functor = pm.AdjustVariables(st.Functor, vars, offset);

		    if (st.Arity == 0) {
			    this.parameters = null;
		    } else {
			    this.parameters = new PrologObject[st.Arity];
		    }

		    for (int np = 0; np <= st.Arity - 1; np++) {
			    this.parameters[np] = pm.AdjustVariables(st.Parameters[np], vars, offset);
		    }
	    }

	    public StructureObject(PrologObject functor, params object[] parameters)
	    {
		    if (functor == null) {
			    throw new ArgumentNullException("functor");
		    }

		    this.functor = functor;

		    if (parameters == null || parameters.Length == 0) {
			    this.parameters = null;
			    return;
		    } 
            else 
            {
			    this.parameters = new PrologObject[parameters.Length];
		    }

		    for (int np = 0; np <= parameters.Length - 1; np++) {
			    this.parameters[np] = Utilities.ToPrologObject(parameters[np]);
		    }
	    }

	    public PrologObject Normalize()
	    {
		    if (Arity == 0) {
			    return Functor;
		    }
		    return this;
	    }

	    public PrologObject Functor {
		    get {
			    return this.functor;
		    }
	    }

	    public int Arity {
		    get {
			    if (this.parameters == null) {
				    return 0;
			    }
			    return this.parameters.Length;
		    }
	    }

	    public PrologObject[] Parameters {
		    get {
			    return this.parameters;
		    }
	    }

	    public override bool Equals(object obj)
	    {
		    PrologObject objMe;

		    objMe = Normalize();

		    if (!(objMe == this)) {
			    return objMe.Equals(obj);
		    }

		    if (obj is StructureObject) {
			    obj = ((StructureObject)(obj)).Normalize();
		    }

		    if (obj == null || !(obj.GetType().Equals(this.GetType()))) {
			    return false;
		    }

		    StructureObject st = ((StructureObject)(obj));

		    if (!(Functor.Equals(st.Functor))) {
			    return false;
		    }

		    if (!(Arity == st.Arity)) {
			    return false;
		    }

		    for (int k = 0; k <= Arity - 1; k++) {
			    if (!(Parameters[k].Equals(st.Parameters[k]))) {
				    return false;
			    }
		    }

		    return true;
	    }

	    public override int GetHashCode()
	    {
		    PrologObject objMe;
		    objMe = Normalize();
		    if (!(objMe == this)) {
			    return objMe.GetHashCode();
		    }
		    int hc = Functor.GetHashCode() ^ Arity;
		    foreach (PrologObject p in this.parameters) {
			    hc = hc ^ p.GetHashCode();
		    }
		    return hc;
	    }

	    public override Node MakeNode(PrologMachine pm)
	    {
		    if (Functor is Primitive) {
			    return new PrimitiveNode(pm, this);
		    }
		    return new FactNode(this, pm);
	    }

	    public override PrologObject Evaluate(PrologMachine pm)
	    {
		    if (Functor is Primitive) {
			    return ((Primitive)(Functor)).Evaluate(pm, this);
		    }
		    return this;
	    }

	    public override string ToString()
	    {
		    if (Functor is Primitive) {
			    return ((Primitive)(Functor)).ToString(Parameters);
		    }
		    if (Arity == 0) {
			    return Functor.ToString();
		    }
		    StringBuilder sb = new StringBuilder(Parameters.Length * 20);
		    sb.Append(Functor.ToString());
		    sb.Append("(");

		    for (int np = 0; np <= Parameters.Length - 1; np++) {
			    if (Parameters[np] == null) {
				    sb.Append("nil");
			    } else {
				    sb.Append(Parameters[np].ToString());
			    }
			    if (np < Parameters.Length - 1) {
				    sb.Append(",");
			    }
		    }
		    sb.Append(")");
		    return sb.ToString();
	    }

	    public override string ToDisplayString()
	    {
		    return GetType().ToString();
	    }
    }
}
