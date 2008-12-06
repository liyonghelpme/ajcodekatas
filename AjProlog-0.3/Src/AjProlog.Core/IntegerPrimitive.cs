using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class IntegerPrimitive : Primitive
    {
        private static IntegerPrimitive instance = new IntegerPrimitive();

        private const string Value = "integer";

        private IntegerPrimitive()
            : base(Value)
        {
        }

        public static IntegerPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            if (pars == null || pars.Length != 1)
                throw new ArgumentException("IntegerPrimitive expects one argument");

            return Utilities.IsInteger(pars[0]);
        }
    }
}
