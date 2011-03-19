namespace DlrHelloWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Scripting;
    using Microsoft.Scripting.Runtime;

    public class HelloScriptCode : ScriptCode
    {
        public HelloScriptCode(SourceUnit sourceUnit)
            : base(sourceUnit)
        {
        }

        public override object Run(Scope scope)
        {
            Console.WriteLine("Hello, World");
            return null;
        }
    }
}
