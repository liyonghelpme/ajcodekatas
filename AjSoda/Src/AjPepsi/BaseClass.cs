namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using AjSoda;

    public class BaseClass : BaseBehavior, IClass
    {
        private List<string> instanceVariableNames;
        private PepsiMachine machine;

        public BaseClass()
        {
            this.instanceVariableNames = new List<string>();
            this.AddMethod("addVariable:", new BaseAddVariableMethod());
            this.AddMethod("basicNew", new BaseBasicNewMethod());
        }

        public BaseClass(IObject behavior)
            : base(behavior)
        {
            this.instanceVariableNames = new List<string>();
        }

        public int InstanceSize
        {
            get
            {
                if (this.Parent is IClass)
                {
                    return ((IClass)this.Parent).InstanceSize + this.instanceVariableNames.Count;
                }

                return this.instanceVariableNames.Count;
            }
        }

        public PepsiMachine Machine
        {
            get
            {
                return this.machine;
            }

            internal set
            {
                this.machine = value;
            }
        }

        public ICollection<string> InstanceVariableNames
        {
            get
            {
                return this.instanceVariableNames;
            }
        }

        public void AddVariable(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (this.instanceVariableNames.Contains(name))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Variable {0} already exists", name));
            }

            this.instanceVariableNames.Add(name);
        }

        public IObject CreateInstance()
        {
            return this.Allocate(this.InstanceSize);
        }

        public override IBehavior CreateDelegated()
        {
            IBehavior delegated = new BaseClass(this.Behavior);
            delegated.Parent = this;

            return delegated;
        }

        public int GetInstanceVariableOffset(string name)
        {
            int offset = this.instanceVariableNames.IndexOf(name);

            if (offset < 0)
                return offset;

            if (this.Parent != null && this.Parent is IClass)
                return offset + ((IClass)this.Parent).InstanceSize;

            return offset;
        }
    }
}
