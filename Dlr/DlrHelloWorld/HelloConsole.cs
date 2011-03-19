namespace DlrHelloWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Scripting.Hosting.Shell;

    public class HelloConsole : ConsoleHost
    {
        protected override Type Provider
        {
            get
            {
                return typeof(HelloContext);
            }
        }
    }
}
