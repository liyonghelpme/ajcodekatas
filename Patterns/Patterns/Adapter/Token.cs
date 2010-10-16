using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Adapter
{
    public class Token
    {
        public TokenType TokenType { get; set; }
        public object Value { get; set; }
    }
}
