using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Patterns.AbstractFactory
{
    public interface IDataService
    {
        IDbConnection CreateConnection(string connectionString);
        IDbCommand CreateTextCommand(string cmdtext, IDbConnection conn);
        IDbCommand CreateSPCommand(string cmdtext, IDbConnection conn);
        IDbDataParameter CreateInputParameter(string name, object value);
        IDbDataParameter CreateOutputParameter(string name, object value);
    }
}
