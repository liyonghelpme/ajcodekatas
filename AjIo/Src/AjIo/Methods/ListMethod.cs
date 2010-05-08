namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class ListMethod : BaseMethod
    {
        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            IObject top = context;

            while (top.Parent != null)
                top = top.Parent;

            return new ListObject(top, arguments);
        }
    }
}
