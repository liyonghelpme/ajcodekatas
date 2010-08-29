using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace HelloWorldIlGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "HelloLibrary";

            AssemblyBuilder assemblyBuilder = currentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("HelloModule");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("HelloClass", TypeAttributes.Public);
            MethodBuilder methodBuilder = typeBuilder.DefineMethod("HelloWorld", MethodAttributes.Public, null, null);

            ILGenerator il = methodBuilder.GetILGenerator();

            il.EmitWriteLine("Hello World");
            il.Emit(OpCodes.Ret);

            Type t = typeBuilder.CreateType();

            object obj = Activator.CreateInstance(t);
            MethodInfo methodInfo = t.GetMethod("HelloWorld");
            methodInfo.Invoke(obj, null);            
        }
    }
}
