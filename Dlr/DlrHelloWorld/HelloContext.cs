namespace DlrHelloWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Scripting.Runtime;
    using Microsoft.Scripting;

    public class HelloContext : LanguageContext
    {
        public HelloContext(ScriptDomainManager domainManager,
            IDictionary<string, object> options)
            : base(domainManager)
        {
        }

        public override ScriptCode CompileSourceCode(SourceUnit sourceUnit, 
            CompilerOptions options, ErrorSink errorSink)
        {
            return new HelloScriptCode(sourceUnit);
        }
    }
}
