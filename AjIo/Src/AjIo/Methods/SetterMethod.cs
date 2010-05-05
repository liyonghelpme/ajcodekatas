namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class SetterMethod : BaseMethod
    {
        private string name;

        public SetterMethod(string name)
        {
            this.name = name;
        }

        public string SlotName { get { return this.name; } }

        public override object Apply(IObject context, IObject receiver, IList<object> arguments)
        {
            if (arguments == null || arguments.Count != 1)
                throw new InvalidOperationException(string.Format("set{0} should have only one argument", this.name));

            receiver.SetSlot(this.name, arguments[0]);

            // TODO review return value
            return arguments[0];
        }
    }
}
