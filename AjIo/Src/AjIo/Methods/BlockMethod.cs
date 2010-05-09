namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Language;

    public class BlockMethod : IMethod
    {
        public object Execute(IObject context, IObject receiver, IList<object> arguments)
        {
            IList<string> names = new List<string>();

            for (int k = 0; k < arguments.Count - 1; k++) 
            {
                Message msg = (Message) arguments[k];
                if (msg.Arguments != null)
                    throw new InvalidOperationException("Invalid parameter in method");
                names.Add(msg.Symbol);
            }

            return new Block(context, (IMessage)arguments.Last(), names);
        }
    }
}
