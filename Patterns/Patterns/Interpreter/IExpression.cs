using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Interpreter
{
    public interface IExpression
    {
        object Evaluate(Context context);
    }
}
