namespace AjClipper.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class Database
    {
        private const string DefaultFactoryName = "System.Data.SqlClient";
        private const string DefaultConnectionStringFormat = "server=.\\SQLEXPRESS;database={0};integrated security=yes";

        private string name;
        private System.Data.Common.DbProviderFactory providerFactory;
        private string connectionString;

        public Database(string name, string factoryname, string connectionString)
        {
            this.name = name;

            if (connectionString == null)
                connectionString = string.Format(DefaultConnectionStringFormat, this.name);

            this.connectionString = connectionString;

            if (factoryname == null)
                factoryname = DefaultFactoryName;

            this.providerFactory = System.Data.Common.DbProviderFactories.GetFactory(factoryname);
        }

        public string Name { get { return this.name; } }

        public string ConnectionString { get { return this.connectionString; } }

        public System.Data.Common.DbProviderFactory ProviderFactory { get { return this.providerFactory; } }

        public IDbConnection GetConnection() 
        {
            IDbConnection connection = this.providerFactory.CreateConnection();
            connection.ConnectionString = this.connectionString;

            return connection;
        }

        public IDbCommand GetCommand(string cmdtext)
        {
            IDbCommand command = this.providerFactory.CreateCommand();
            command.CommandText = cmdtext;
            command.Connection = this.GetConnection();

            return command;
        }
    }
}
