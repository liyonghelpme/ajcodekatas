using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpreter
{
    public interface IExpression
    {
        object Evaluate();
    }
}
