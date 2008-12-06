using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class AtomPrimitive : Primitive
    {
        private static AtomPrimitive instance = new AtomPrimitive();

        private const string Value = "atom";

        private AtomPrimitive()
            : base(Value)
        {
        }

        public static AtomPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            if (pars == null || pars.Length != 1)
                throw new ArgumentException("AtomPrimitive expects one argument");

            return Utilities.IsAtom(pars[0]);
        }
    }
}
