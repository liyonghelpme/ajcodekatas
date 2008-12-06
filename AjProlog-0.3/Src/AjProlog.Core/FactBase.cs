using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace AjProlog.Core
{
    public class FactBase
    {
        private IList mFacts = new ArrayList();

        public void Add(PrologObject fact)
        {
            if (!(mFacts.Contains(fact)))
            {
                mFacts.Add(fact);
            }
        }

        public IList GetFacts(PrologObject po)
        {
            return mFacts;
        }

        private bool IsPredicate(PrologObject fact, StringObject atom)
        {
            if (fact.Equals(atom))
            {
                return true;
            }
            if (!(fact is StructureObject))
            {
                return false;
            }
            StructureObject st = ((StructureObject)(fact));
            if (st.Functor.Equals(atom))
            {
                return true;
            }
            if (!(st.Functor == IfPrimitive.GetInstance))
            {
                return false;
            }
            fact = st.Parameters(0);
            if (fact.Equals(atom))
            {
                return true;
            }
            if (!(fact is StructureObject))
            {
                return false;
            }
            st = ((StructureObject)(fact));
            if (st.Functor.Equals(atom))
            {
                return true;
            }
            return false;
        }

        public IList GetPredicates(StringObject atom)
        {
            ArrayList result = new ArrayList();
            PrologObject fact;
            foreach (Fact fact in mFacts)
            {
                if (IsPredicate(fact, atom))
                {
                    result.Add(fact);
                }
            }
            return result;
        }
    }
}
