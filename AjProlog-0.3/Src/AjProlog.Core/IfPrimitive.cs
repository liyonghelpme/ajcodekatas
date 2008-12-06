using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class IfPrimitive : Primitive
    {
        private static IfPrimitive instance = new IfPrimitive();

        private const string Value = ":=";

        private IfPrimitive()
            : base(Value)
        {
        }

        public static IfPrimitive GetInstance()
        {
            return instance;
        }

        public override bool Execute(PrologMachine pm, PrologObject[] pars)
        {
            return true;
        }

        public virtual new string ToString(PrologObject[] parameters)
        {
            if (parameters == null || parameters.Length != 2)
            {
                return base.ToString(parameters);
            }

            StringBuilder sb = new StringBuilder(50);

            sb.Append(parameters[0].ToString());
            sb.Append(Value);
            sb.Append(parameters[1].ToString());

            return sb.ToString();
        }
    }
}
