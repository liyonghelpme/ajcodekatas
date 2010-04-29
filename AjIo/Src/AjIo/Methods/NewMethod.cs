namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NewMethod : BaseNativeMethod
    {
        public override object Apply(AjIo.Language.IObject context, object receiver, IList<object> arguments)
        {
            Type type = (Type)receiver;

            if (arguments == null)
                return Activator.CreateInstance(type);

            return Activator.CreateInstance(type, arguments.ToArray());
        }
    }
}
