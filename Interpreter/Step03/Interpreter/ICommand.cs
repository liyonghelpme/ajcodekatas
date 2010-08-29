using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public interface ICommand
    {
        void Execute(BindingEnvironment environment);
    }
}
