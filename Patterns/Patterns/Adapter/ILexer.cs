using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Adapter
{
    public interface ILexer
    {
        Token ReadToken();
    }
}
