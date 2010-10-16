using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.AbstractFactory
{
    public class SqlClientDataService : BaseDataService
    {
        public SqlClientDataService()
            : base("System.Data.SqlClient")
        {
        }
    }
}
