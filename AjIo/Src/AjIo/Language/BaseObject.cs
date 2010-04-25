namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseObject : IObject
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

        public virtual object Process(IObject context, Message message)
        {
            object value = this.GetSlot(message.Symbol);

            if (message.Arguments == null && !(value is IMethod))
                return value;

            IMethod method = (IMethod) value;

            if (method == null)
                throw new InvalidOperationException(string.Format("Undefined method '{0}'", message.Symbol));

            if (message.Arguments == null)
                return method.Execute(context, this, message.Arguments);

            IList<object> parameters = new List<object>();

            foreach (object arg in message.Arguments)
                parameters.Add(context.Evaluate(arg));

            return method.Execute(context, this, parameters);
        }

        public override string ToString()
        {
            return string.Format("{0}_{1:x}", this.TypeName, this.GetHashCode());
        }

        public abstract string TypeName { get; }

        public object Evaluate(object expression)
        {
            Message message = expression as Message;

            if (message != null)
                return this.Process(this, message);

            ICollection<Message> messages = expression as ICollection<Message>;

            if (messages != null)
            {
                object receiver = this;

                foreach (Message msg in messages)
                    receiver = ((IObject)receiver).Process(this, msg);

                return receiver;
            }

            return expression;
        }
    }
}
