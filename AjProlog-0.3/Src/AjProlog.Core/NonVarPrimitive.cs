using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class NonVarPrimitive : Primitive
    {
        private static NonVarPrimitive instance = new NonVarPrimitive();

        private const string Value = "nonvar";

        private NonVarPrimitive()
            : base(Value)
        {
        }

        public static NonVarPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            if (pars == null || pars.Length != 1)
                throw new ArgumentException("NonVarPrimitive expects one argument");

            return !Utilities.IsVariable(pars[0]);
        }
    }
}
