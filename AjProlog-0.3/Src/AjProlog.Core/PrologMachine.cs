using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace AjProlog.Core
{
    public class PrologMachine
    {
        private FactBase facts = new FactBase();
        private IList variables = new ArrayList();
        private IList bindings = new ArrayList();
        private IList pendings = new ArrayList();
        private IList nodes = new ArrayList();
        private IList queryVariables = new ArrayList();
        private int level;
        private bool trace;
        private TextReader input = Console.In;
        private TextWriter output = Console.Out;
        private bool interactive;
        private Node node;
        private PrologMachineAction action;

        public bool Interactive
        {
            get
            {
                return interactive;
            }
            set
            {
                interactive = value;
            }
        }

        public bool Trace
        {
            get
            {
                return trace;
            }
            set
            {
                trace = true;
            }
        }

        public TextReader Input
        {
            get
            {
                return input;
            }
        }

        public TextWriter Output
        {
            get
            {
                return output;
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        public int NVariables
        {
            get
            {
                return variables.Count;
            }
            set
            {
                while (variables.Count > value)
                {
                    variables.RemoveAt(variables.Count - 1);
                }
                while (variables.Count < value)
                {
                    variables.Add(new Variable(variables.Count, this));
                }
            }
        }

        public int NBindings
        {
            get
            {
                return bindings.Count;
            }
            set
            {
                Variable v;
                while (bindings.Count > value)
                {
                    v = (Variable) bindings[bindings.Count - 1];
                    v.Unbind();
                    bindings.RemoveAt(bindings.Count - 1);
                }
            }
        }

        public int NPendings
        {
            get
            {
                return pendings.Count;
            }
            set
            {
                while (pendings.Count > value)
                {
                    pendings.RemoveAt(pendings.Count - 1);
                }
                if (pendings.Count < value)
                {
                    throw new Exception("Pocos Pendientes");
                }
            }
        }

        public IList Bindings
        {
            get
            {
                return bindings;
            }
        }
        public Node PopNode()
        {
            if (nodes.Count == 0)
            {
                return null;
            }
            Node node;
            node = (Node) nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);
            return node;
        }

        public void PushNode(Node node)
        {
            if (!(node == null && node.IsPushable()))
            {
                nodes.Add(node);
            }
        }

        public void PushPending(PrologObject po)
        {
            if (!(po == null))
            {
                pendings.Add(po.MakeNode(this));
            }
        }

        public void PushPending(Node node)
        {
            if (!(node == null))
            {
                pendings.Add(node);
            }
        }

        public Node PopPending()
        {
            if (pendings.Count == 0)
            {
                return null;
            }
            Node n;
            n = (Node) pendings[pendings.Count - 1];
            pendings.RemoveAt(pendings.Count - 1);
            return n;
        }

        public void Assertz(PrologObject po)
        {
            facts.Add(po);
        }

        public IList GetFacts(PrologObject po)
        {
            return facts.GetFacts(po);
        }

        public IList GetPredicates(StringObject atom)
        {
            return facts.GetPredicates(atom);
        }

        bool DoSolution()
        {
            if (queryVariables.Count == 0)
            {
                Console.WriteLine("yes");
                return true;
            }
            for (int k = 0; k <= queryVariables.Count - 1; k++)
            {
                Console.WriteLine(queryVariables[k].ToString() + ": " + GetVariable(k).Dereference().ToString());
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
            Console.WriteLine("Level " + Level);
            Console.WriteLine("Variables");
            for (int n = 0; n <= NVariables - 1; n++)
            {
                Console.WriteLine("n: " + GetVariable(n).Dereference().ToString());
            }
            Console.WriteLine("Pending");
            for (int n = 0; n <= pendings.Count - 1; n++)
            {
                Console.WriteLine(((Node)(pendings[n])).Object.ToString());
            }
            Console.WriteLine("Nodes");
            for (int n = 0; n <= nodes.Count - 1; n++)
            {
                Console.WriteLine(((Node)(nodes[n])).Object.ToString());
            }
        }

        public bool Resolve(PrologObject po)
        {
            variables.Clear();
            bindings.Clear();
            pendings.Clear();
            nodes.Clear();
            queryVariables.Clear();
            level = 0;
            ArrayList vars = new ArrayList();
            po = AdjustVariables(po, vars, variables.Count);
            if (Interactive)
            {
                queryVariables = vars;
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
                throw new ArgumentException("Incorrect number of variables: " + n);
            }
            if (n >= variables.Count)
            {
                for (int k = variables.Count; k <= n; k++)
                {
                    variables.Add(new Variable(k, this));
                }
            }
            return (Variable) variables[n];
        }

        public PrologObject AdjustVariables(PrologObject po, ArrayList vars, int offset)
        {
            if (po is StructureObject)
            {
                po = ((StructureObject)po).Normalize();
            }

            if (po is StringObject && ((StringObject)po).IsVariableName())
            {
                int off;

                off = vars.IndexOf(po);

                if (off >= 0)
                {
                    return GetVariable(off + offset);
                }

                vars.Add(po);

                po = GetVariable(offset + vars.Count - 1);
            }
            else if (po is StructureObject) 
            {
                po = new StructureObject((StructureObject) po, this, vars, offset);
            }

            return po;
        }

        public PrologObject AdjustVariables(PrologObject po)
        {
            return AdjustVariables(po, new ArrayList(), variables.Count);
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
            this.node = node;
            this.action = action;
        }

        public FactNode GetCutNode()
        {
            for (int k = nodes.Count - 1; k >= 0; k--)
            {
                if (nodes[k] is FactNode)
                {
                    return ((FactNode)(nodes[k]));
                }
            }
            return null;
        }

        public void CutToNode(FactNode node)
        {
            Node n;
            while (nodes.Count > 0)
            {
                n = (Node) nodes[nodes.Count - 1];
                if (n == node)
                {
                    ((FactNode)(node)).DoCut();
                    return;
                }
                nodes.RemoveAt(nodes.Count - 1);
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
