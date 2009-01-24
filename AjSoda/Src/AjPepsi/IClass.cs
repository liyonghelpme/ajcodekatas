namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjSoda;

    public interface IClass : IBehavior
    {
        int InstanceSize { get; }

        ICollection<string> InstanceVariableNames { get; }

        void AddVariable(string name);

        IObject CreateInstance();

        int GetInstanceVariableOffset(string name);
    }
}
