namespace AjIo.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjIo.Methods;
    using AjIo.Methods.Arithmetic;
    using AjIo.Methods.Comparison;
    using AjIo.Utilities;

    public class Message : AjIo.Language.IMessage
    {
        private static Dictionary<string, INativeMethod> globalMethods = new Dictionary<string, INativeMethod>();

        private string symbol;
        private IList<object> arguments;

        static Message()
        {
            globalMethods["+"] = new AddMethod();
            globalMethods["-"] = new SubtractMethod();
            globalMethods["*"] = new MultiplyMethod();
            globalMethods["/"] = new DivideMethod();
            globalMethods["=="] = new EqualsNativeMethod();

            // TODO put not in global, but associated with types
            globalMethods["new"] = new NewMethod();
        }

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
            {
                // TODO review this way
                // Check for native type
                if (result == null && this.symbol.Contains("."))
                {
                    Type type = TypeUtilities.AsType(this.symbol);

                    if (type != null)
                        return type;
                }

                return result;
            }

            IMethod method = (IMethod)result;

            return method.Execute(context, receiver.Self, this.arguments);
        }

        public object Send(IObject context, object receiver)
        {
            if (receiver is IObject)
                return this.Send(context, (IObject)receiver);

            if (globalMethods.ContainsKey(this.symbol))
            {
                INativeMethod method = globalMethods[this.symbol];
                return method.Execute(context, receiver, this.arguments);
            }

            Type type = receiver.GetType();
            object[] parameters = null;

            if (this.arguments != null)
                parameters = this.arguments.ToArray();

            return type.InvokeMember(this.symbol, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance, null, receiver, parameters);
        }
    }
}
