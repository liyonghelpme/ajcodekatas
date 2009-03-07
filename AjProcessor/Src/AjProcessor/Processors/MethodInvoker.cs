namespace AjProcessor.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MethodInvoker : BaseProcessor
    {
        public object Object { get; set; }
        public string MethodName { get; set; }

        public override void ProcessMessage(Message message)
        {
            object result = this.Object.GetType().InvokeMember(this.MethodName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, this.Object, new object[] { message.Payload });

            if (result != null)
                this.PostMessage(new Message(result));
        }
    }
}
