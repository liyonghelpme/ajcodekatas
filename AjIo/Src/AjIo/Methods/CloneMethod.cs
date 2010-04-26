namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class CloneMethod : BaseMethod
    {
        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            if (arguments != null)
                throw new InvalidOperationException("clone should have no arguments");

            return new ClonedObject(receiver);
        }
    }
}
