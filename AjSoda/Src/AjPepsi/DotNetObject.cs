namespace AjPepsi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public static class DotNetObject
    {
        public static object NewObject(Type type, object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        public static object SendMessage(object receiver, string methodName, object[] args)
        {
            return receiver.GetType().InvokeMember(methodName, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, null, receiver, args, CultureInfo.InvariantCulture);
        }
    }
}
