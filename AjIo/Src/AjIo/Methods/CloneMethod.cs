﻿namespace AjIo.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AjIo.Language;

    public class CloneMethod : IMethod
    {
        public object Execute(IObject receiver, IList<object> arguments)
        {
            if (arguments != null)
                throw new InvalidOperationException("clone should have no arguments");

            return new ClonedObject(receiver);
        }
    }
}