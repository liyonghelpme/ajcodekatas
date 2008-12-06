using System;
using System.Collections.Generic;
using System.Text;

namespace AjProlog.Core
{
    public class Primitives
    {
        private static Dictionary<string, Primitive> primitives = new Dictionary<string, Primitive>();

        static Primitives()
        {
            Register(AndPrimitive.GetInstance());
            //Register(QueryPrimitive.GetInstance());
            Register(IfPrimitive.GetInstance());
            //Register(TruePrimitive.GetInstance());
            //Register(FailPrimitive.GetInstance());
            Register(VarPrimitive.GetInstance());
            Register(NonVarPrimitive.GetInstance());
            Register(AtomPrimitive.GetInstance());
            Register(AtomicPrimitive.GetInstance());
            Register(IntegerPrimitive.GetInstance());
            //Register(ListingPrimitive.GetInstance());
            //Register(WritePrimitive.GetInstance());
            //Register(NlPrimitive.GetInstance());
            //Register(TabPrimitive.GetInstance());
            //Register(DisplayPrimitive.GetInstance());
            //Register(CutPrimitive.GetInstance());
            Register(EqualPrimitive.GetInstance());
            //Register(NotEqualPrimitive.GetInstance());
            //Register(LessPrimitive.GetInstance());
            //Register(PlusPrimitive.GetInstance());
            //Register(MinusPrimitive.GetInstance());
            //Register(IsPrimitive.GetInstance());
        }

        public static void Register(Primitive primitive)
        {
            primitives[primitive.ToString()] = primitive;
        }

        public static Primitive GetPrimitive(string name)
        {
            if (!primitives.ContainsKey(name))
                return null;

            return primitives[name];
        }
    }
}

