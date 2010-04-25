namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BaseObject : IObject
    {
        protected Dictionary<string, object> slotValues = new Dictionary<string, object>();

        public virtual void SetSlot(string name, object value)
        {
            this.slotValues[name] = value;
        }

        public virtual object GetSlot(string name)
        {
            if (this.slotValues.ContainsKey(name))
                return this.slotValues[name];

            return null;
        }

        public virtual object Process(Message message)
        {
            IMethod method = (IMethod) this.GetSlot(message.Symbol);

            if (method == null)
                throw new InvalidOperationException(string.Format("Undefined method '{0}'", message.Symbol));

            return method.Execute(this, message.Arguments);
        }
    }
}
