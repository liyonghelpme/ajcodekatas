using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace AjProlog.Core
{
    public class PrologMachine
    {
        private FactBase mFacts = new FactBase();
        private IList mVariables = new ArrayList();
        private IList mBindings = new ArrayList();
        private IList mPendings = new ArrayList();
        private IList mNodes = new ArrayList();
        private IList mQueryVariables = new ArrayList();
        private int mLevel;
        private bool mTrace;
        private TextReader mInput = Console.In;
        private TextWriter mOutput = Console.Out;
        private bool mInteractive;
        private Node mNode;
        private PrologMachineAction mAction;

        public bool Interactive
        {
            get
            {
                return mInteractive;
            }
            set
            {
                mInteractive = value;
            }
        }

        public bool Trace
        {
            get
            {
                return mTrace;
            }
            set
            {
                mTrace = true;
            }
        }

        public TextReader Input
        {
            get
            {
                return mInput;
            }
        }

        public TextWriter Output
        {
            get
            {
                return mOutput;
            }
        }

        public int Level
        {
            get
            {
                return mLevel;
            }
            set
            {
                mLevel = value;
            }
        }

        public int NVariables
        {
            get
            {
                return mVariables.Count;
            }
            set
            {
                while (mVariables.Count > value)
                {
                    mVariables.RemoveAt(mVariables.Count - 1);
                }
                while (mVariables.Count < value)
                {
                    mVariables.Add(new Variable(mVariables.Count, this));
                }
            }
        }

        public int NBindings
        {
            get
            {
                return mBindings.Count;
            }
            set
            {
                Variable v;
                while (mBindings.Count > value)
                {
                    v = (Variable) mBindings[mBindings.Count - 1];
                    v.Unbind();
                    mBindings.RemoveAt(mBindings.Count - 1);
                }
            }
        }

        public int NPendings
        {
            get
            {
                return mPendings.Count;
            }
            set
            {
                while (mPendings.Count > value)
                {
                    mPendings.RemoveAt(mPendings.Count - 1);
                }
                if (mPendings.Count < value)
                {
                    throw new Exception("Pocos Pendientes");
                }
            }
        }

        public IList Bindings
        {
            get
            {
                return mBindings;
            }
        }
        public Node PopNode()
        {
            if (mNodes.Count == 0)
            {
                return null;
            }
            Node node;
            node = (Node) mNodes[mNodes.Count - 1];
            mNodes.RemoveAt(mNodes.Count - 1);
            return node;
        }

        public void PushNode(Node node)
        {
            if (!(node == null && node.IsPushable()))
            {
                mNodes.Add(node);
            }
        }

        public void PushPending(PrologObject po)
        {
            if (!(po == null))
            {
                mPendings.Add(po.MakeNode(this));
            }
        }

        public void PushPending(Node node)
        {
            if (!(node == null))
            {
                mPendings.Add(node);
            }
        }

        public Node PopPending()
        {
            if (mPendings.Count == 0)
            {
                return null;
            }
            Node n;
            n = (Node) mPendings[mPendings.Count - 1];
            mPendings.RemoveAt(mPendings.Count - 1);
            return n;
        }

        public void Assertz(PrologObject po)
        {
            mFacts.Add(po);
        }

        public IList GetFacts(PrologObject po)
        {
            return mFacts.GetFacts(po);
        }

        public IList GetPredicates(StringObject atom)
        {
            return mFacts.GetPredicates(atom);
        }

        bool DoSolution()
        {
            if (mQueryVariables.Count == 0)
            {
                Console.WriteLine("yes");
                return true;
            }
            for (int k = 0; k <= mQueryVariables.Count - 1; k++)
            {
                Console.WriteLine(mQueryVariables[k].ToString() + ": " + GetVariable(k).Dereference().ToString());
            }
            while (Console.In.Peek() > 0)
            {
                Console.In.Read();
            }
            string line = Console.In.ReadLine();
            if (line == ";")
            {
                return false;
            }
            return true;
        }

        void DoNoSolution()
        {
            Console.WriteLine("no");
        }

        void ExecuteTrace()
        {
            Console.WriteLine("Nivel " + Level);
            Console.WriteLine("Variables");
            for (int n = 0; n <= NVariables - 1; n++)
            {
                Console.WriteLine("n: " + GetVariable(n).Dereference().ToString());
            }
            Console.WriteLine("Pendientes");
            for (int n = 0; n <= mPendings.Count - 1; n++)
            {
                Console.WriteLine(((Node)(mPendings[n])).Object.ToString());
            }
            Console.WriteLine("Nodos");
            for (int n = 0; n <= mNodes.Count - 1; n++)
            {
                Console.WriteLine(((Node)(mNodes[n])).Object.ToString());
            }
        }

        public bool Resolve(PrologObject po)
        {
            mVariables.Clear();
            mBindings.Clear();
            mPendings.Clear();
            mNodes.Clear();
            mQueryVariables.Clear();
            mLevel = 0;
            ArrayList vars = new ArrayList();
            po = AdjustVariables(po, vars, mVariables.Count);
            if (Interactive)
            {
                mQueryVariables = vars;
            }
            PushPending(po);
            Node node;
            bool result;
            result = true;
            while (true)
            {
                if (Trace)
                {
                    ExecuteTrace();
                }
                if (result)
                {
                    node = PopPending();
                    if (node == null)
                    {
                        if (Interactive)
                        {
                            if (DoSolution())
                            {
                                return true;
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        result = node.ExecuteCall();
                    }
                }
                else
                {
                    node = PopNode();
                    if (node == null)
                    {
                        if (Interactive)
                        {
                            DoNoSolution();
                        }
                        return false;
                    }
                    result = node.ExecuteRedo();
                }
            }
        }

        public Variable GetVariable(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Nro. de Variable incorrecto " + n);
            }
            if (n >= mVariables.Count)
            {
                for (int k = mVariables.Count; k <= n; k++)
                {
                    mVariables.Add(new Variable(k, this));
                }
            }
            return (Variable) mVariables[n];
        }

        public bool Unify(PrologObject po1, PrologObject po2)
        {
            po1 = po1.Dereference();
            po2 = po2.Dereference();
            if (po1.Equals(po2))
            {
                return true;
            }
            if (po1 is Variable)
            {
                ((Variable)(po1)).Bind(po2);
                return true;
            }
            if (po2 is Variable)
            {
                ((Variable)(po2)).Bind(po1);
                return true;
            }
            if (po1 is StructureObject && po2 is StructureObject)
            {
                StructureObject st1 = ((StructureObject)(po1));
                StructureObject st2 = ((StructureObject)(po2));
                if (!(Unify(st1.Functor, st2.Functor)))
                {
                    return false;
                }
                if (!(st1.Arity == st2.Arity))
                {
                    return false;
                }
                for (int k = 0; k <= st1.Arity - 1; k++)
                {
                    if (!(Unify(st1.Parameters[k], st2.Parameters[k])))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public void NextAction(Node node, PrologMachineAction action)
        {
            mNode = node;
            mAction = action;
        }

        public FactNode GetCutNode()
        {
            for (int k = mNodes.Count - 1; k >= 0; k--)
            {
                if (mNodes[k] is FactNode)
                {
                    return ((FactNode)(mNodes[k]));
                }
            }
            return null;
        }

        public void CutToNode(FactNode node)
        {
            Node n;
            while (mNodes.Count > 0)
            {
                n = (Node) mNodes[mNodes.Count - 1];
                if (n == node)
                {
                    ((FactNode)(node)).DoCut();
                    return;
                }
                mNodes.RemoveAt(mNodes.Count - 1);
            }
        }

        public PrologMachineStatus Status
        {
            get
            {
                PrologMachineStatus st = new PrologMachineStatus();
                st.NVariables = NVariables;
                st.NBindings = NBindings;
                st.NPendings = NPendings;
                st.Level = Level;
                return st;
            }
            set
            {
                NVariables = value.NVariables;
                NBindings = value.NBindings;
                NPendings = value.NPendings;
                Level = value.Level;
            }
        }
    }

    public enum PrologMachineAction
    {
        Call = 1,
        Exit = 2,
        Redo = 3,
        Fail = 4
    }

    public class PrologMachineStatus
    {
        public int NVariables;
        public int NBindings;
        public int NPendings;
        public int Level;
    }
}
