using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class AtomicPrimitive : Primitive
    {
        private static AtomicPrimitive instance = new AtomicPrimitive();

        private const string Value = "atomic";

        private AtomicPrimitive()
            : base(Value)
        {
        }

        public static AtomicPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            if (pars == null || pars.Length != 1)
                throw new ArgumentException("AtomicPrimitive expects one argument");

            return Utilities.IsAtomic(pars[0]);
        }
    }
}
