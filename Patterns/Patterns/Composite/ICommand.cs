using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Interpreter;

namespace Patterns.Composite
{
    public interface ICommand
    {
        void Execute(Context context);
    }
}
