using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class VarPrimitive : Primitive
    {
        private static VarPrimitive instance = new VarPrimitive();

        private const string Value = "var";

        private VarPrimitive()
            : base(Value)
        {
        }

        public static VarPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            if (pars == null || pars.Length != 1)
                throw new ArgumentException("VarPrimitive expects one argument");

            return Utilities.IsVariable(pars[0]);
        }
    }
}
