namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjIo.Methods.Arithmetic;

    public class Message : AjIo.Language.IMessage
    {
        private string symbol;
        private IList<object> arguments;

        public Message(string symbol)
        {
            this.symbol = symbol;
        }

        public Message(string symbol, IList<object> arguments)
        {
            this.symbol = symbol;
            this.arguments = arguments;
        }

        public string Symbol { get { return this.symbol; } }

        public IList<object> Arguments { get { return this.arguments; } }

        public object Send(IObject context, IObject receiver)
        {
            object result;

            result = receiver.GetSlot(this.symbol);

            if (this.arguments == null && !(result is IMethod))
                return result;

            IMethod method = (IMethod)result;

            return method.Execute(context, receiver.Self, this.arguments);
        }

        public object Send(IObject context, object receiver)
        {
            if (receiver is IObject)
                return this.Send(context, (IObject)receiver);

            // TODO refactor
            if (this.symbol == "+")
                return (new AddMethod()).Execute(context, receiver, this.arguments);

            throw new InvalidOperationException(string.Format("Unknown method '{0}'", this.symbol));
        }
    }
}
