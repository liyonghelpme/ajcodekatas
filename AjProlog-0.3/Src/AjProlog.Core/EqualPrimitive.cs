using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class EqualPrimitive : Primitive
    {
        private static EqualPrimitive instance = new EqualPrimitive();

        private const string Value = "=";

        private EqualPrimitive()
            : base(Value)
        {
        }

        public static EqualPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            if (pars == null || pars.Length != 2)
                throw new ArgumentException("EqualPrimitive expects two arguments");

            return pm.Unify(pars[0], pars[1]);
        }

        public override Node MakeNode(PrologMachine pm)
        {
            return new PrimitiveStatusNode(pm, this);
        }
    }
}
