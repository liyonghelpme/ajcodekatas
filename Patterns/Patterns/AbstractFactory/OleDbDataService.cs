using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.AbstractFactory
{
    public class OleDbDataService : BaseDataService
    {
        public OleDbDataService()
            : base("System.Data.OleDb")
        {
        }
    }
}
