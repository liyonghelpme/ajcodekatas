namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IBehavior : IObject
    {
        IDictionary<string, IMethod> Methods { get; }

        IObject Parent { get; set; }

        IMethod Lookup(string selector);

        void AddMethod(string selector, IMethod method);

        IBehavior CreateDelegated();
    }
}
