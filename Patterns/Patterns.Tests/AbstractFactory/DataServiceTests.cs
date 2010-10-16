using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.AbstractFactory;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Patterns.Tests.AbstractFactory
{
    [TestClass]
    public class DataServiceTests
    {
        private const string SqlConnection = "server=(local);database=AjFirstExample;Integrated Security=true";
        private const string OleDbConnection = "Provider=SQLOLEDB;Data Source=localhost;Initial Catalog=AjFirstExample;Integrated Security=true;";
        private const string SelectText = "select Id, Name, Address, Notes from ajfe_customers";
        private const string SPName = "CustomerGetAll";

        [TestMethod]
        public void CreateSqlConnection()
        {
            IDataService service = new SqlClientDataService();
            IDbConnection connection = service.CreateConnection(SqlConnection);

            Assert.IsNotNull(connection);
            Assert.IsInstanceOfType(connection, typeof(SqlConnection));
            Assert.AreEqual(SqlConnection, connection.ConnectionString);
        }

        [TestMethod]
        public void CreateOleDbConnection()
        {
            IDataService service = new OleDbDataService();
            IDbConnection connection = service.CreateConnection(OleDbConnection);

            Assert.IsNotNull(connection);
            Assert.IsInstanceOfType(connection, typeof(OleDbConnection));
            Assert.AreEqual(OleDbConnection, connection.ConnectionString);
        }

        [TestMethod]
        public void CreateSqlTextCommand()
        {
            IDataService service = new SqlClientDataService();
            IDbConnection connection = service.CreateConnection(SqlConnection);
            IDbCommand command = service.CreateTextCommand(SelectText, connection);

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SqlCommand));
            Assert.AreEqual(SelectText, command.CommandText);
            Assert.AreEqual(CommandType.Text, command.CommandType);
        }

        [TestMethod]
        public void CreateOleDbTextCommand()
        {
            IDataService service = new OleDbDataService();
            IDbConnection connection = service.CreateConnection(OleDbConnection);
            IDbCommand command = service.CreateTextCommand(SelectText, connection);

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(OleDbCommand));
            Assert.AreEqual(SelectText, command.CommandText);
            Assert.AreEqual(CommandType.Text, command.CommandType);
        }

        [TestMethod]
        public void CreateSqlSPCommand()
        {
            IDataService service = new SqlClientDataService();
            IDbConnection connection = service.CreateConnection(SqlConnection);
            IDbCommand command = service.CreateSPCommand(SPName, connection);

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(SqlCommand));
            Assert.AreEqual(SPName, command.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, command.CommandType);
        }

        [TestMethod]
        public void CreateOleDbSPCommand()
        {
            IDataService service = new OleDbDataService();
            IDbConnection connection = service.CreateConnection(OleDbConnection);
            IDbCommand command = service.CreateSPCommand(SPName, connection);

            Assert.IsNotNull(command);
            Assert.IsInstanceOfType(command, typeof(OleDbCommand));
            Assert.AreEqual(SPName, command.CommandText);
            Assert.AreEqual(CommandType.StoredProcedure, command.CommandType);
        }

        [TestMethod]
        public void CreateSqlInputParameter()
        {
            IDataService service = new SqlClientDataService();
            IDbConnection connection = service.CreateConnection(SqlConnection);
            IDbDataParameter parameter = service.CreateInputParameter("Id", 1);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOfType(parameter, typeof(SqlParameter));
            Assert.AreEqual("@Id", parameter.ParameterName);
            Assert.AreEqual(1, parameter.Value);
            Assert.AreEqual(ParameterDirection.Input, parameter.Direction);
        }

        [TestMethod]
        public void CreateOleDbInputParameter()
        {
            IDataService service = new OleDbDataService();
            IDbConnection connection = service.CreateConnection(OleDbConnection);
            IDbDataParameter parameter = service.CreateInputParameter("Id", 1);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOfType(parameter, typeof(OleDbParameter));
            Assert.AreEqual("@Id", parameter.ParameterName);
            Assert.AreEqual(1, parameter.Value);
            Assert.AreEqual(ParameterDirection.Input, parameter.Direction);
        }

        [TestMethod]
        public void CreateSqlOutputParameter()
        {
            IDataService service = new SqlClientDataService();
            IDbConnection connection = service.CreateConnection(SqlConnection);
            IDbDataParameter parameter = service.CreateOutputParameter("Id", 1);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOfType(parameter, typeof(SqlParameter));
            Assert.AreEqual("@Id", parameter.ParameterName);
            Assert.AreEqual(1, parameter.Value);
            Assert.AreEqual(ParameterDirection.Output, parameter.Direction);
        }

        [TestMethod]
        public void CreateOleDbOutputParameter()
        {
            IDataService service = new OleDbDataService();
            IDbConnection connection = service.CreateConnection(OleDbConnection);
            IDbDataParameter parameter = service.CreateOutputParameter("Id", 1);

            Assert.IsNotNull(parameter);
            Assert.IsInstanceOfType(parameter, typeof(OleDbParameter));
            Assert.AreEqual("@Id", parameter.ParameterName);
            Assert.AreEqual(1, parameter.Value);
            Assert.AreEqual(ParameterDirection.Output, parameter.Direction);
        }
    }
}
