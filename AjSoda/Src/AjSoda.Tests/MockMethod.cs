namespace AjSoda.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjSoda;

    internal class MockMethod : IMethod
    {
        public bool Executed { get; private set; }

        public object Execute(object receiver, object[] arguments)
        {
            this.Executed = true;
            return null;
        }
    }
}
