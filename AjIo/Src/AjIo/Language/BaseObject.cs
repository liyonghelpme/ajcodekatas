namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjIo.Methods;

    public abstract class BaseObject : IObject
    {
        protected Dictionary<string, object> slotValues = new Dictionary<string, object>();

        public abstract string TypeName { get; }

        public virtual IObject Self
        {
            get
            {
                return this;
            }
        }

        public abstract IObject Parent { get; }

        public virtual void SetSlot(string name, object value)
        {
            this.slotValues[name] = value;
        }

        public void SetMethodSlot(string name, Func<IObject, IObject, IList<object>, object> function)
        {
            this.slotValues[name] = new FunctionMethod(function);
        }

        public virtual void UpdateSlot(string name, object value)
        {
            if (this.slotValues.ContainsKey(name))
            {
                this.slotValues[name] = value;
                return;
            }

            throw new InvalidOperationException(string.Format("Not defined slot '{0}'", name));
        }

        public virtual object GetSlot(string name)
        {
            if (this.slotValues.ContainsKey(name))
                return this.slotValues[name];

            return null;
        }

        public override string ToString()
        {
            return string.Format("{0}_{1:x}", this.TypeName, this.GetHashCode());
        }

        public object Evaluate(object expression)
        {
            IMessage message = expression as IMessage;

            if (message != null)
                return message.Send(this, this);

            return expression;
        }
    }
}
