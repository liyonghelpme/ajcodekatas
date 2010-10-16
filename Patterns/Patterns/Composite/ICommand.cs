using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;
using Patterns.Visitor;

namespace Patterns.Composite
{
    public interface ICommand
    {
        void Execute(IContext context);

        void Accept(IVisitor visitor);
    }
}
