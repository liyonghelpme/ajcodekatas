using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Patterns.AbstractFactory
{
    public abstract class BaseDataService : IDataService
    {
        private DbProviderFactory factory;

        public BaseDataService(string providerName)
        {
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        public virtual IDbConnection CreateConnection(string connectionString)
        {
            IDbConnection connection = this.factory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public virtual IDbCommand CreateTextCommand(string cmdtext, IDbConnection conn)
        {
            return this.CreateCommand(cmdtext, conn, CommandType.Text);
        }

        public virtual IDbCommand CreateSPCommand(string cmdtext, IDbConnection conn)
        {
            return this.CreateCommand(cmdtext, conn, CommandType.StoredProcedure);
        }

        public virtual IDbDataParameter CreateInputParameter(string name, object value)
        {
            return this.CreateParameter(name, value, ParameterDirection.Input);
        }

        public virtual IDbDataParameter CreateOutputParameter(string name, object value)
        {
            return this.CreateParameter(name, value, ParameterDirection.Output);
        }

        protected virtual IDbCommand CreateCommand(string cmdtext, IDbConnection conn, CommandType type)
        {
            IDbCommand command = this.factory.CreateCommand();
            command.Connection = conn;
            command.CommandType = type;
            command.CommandText = cmdtext;
            return command;
        }

        protected virtual IDbDataParameter CreateParameter(string name, object value, ParameterDirection direction)
        {
            IDbDataParameter parameter = this.factory.CreateParameter();
            parameter.ParameterName = "@" + name;
            parameter.Value = value;
            parameter.Direction = direction;
            return parameter;
        }
    }
}
