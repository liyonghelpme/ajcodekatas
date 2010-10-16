using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Composite;
using Patterns.Interpreter;

namespace Patterns.Adapter
{
    public interface IParser
    {
        ICommand ParseCommand();
        IExpression ParseExpression();
    }
}
