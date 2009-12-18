using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjClipper.Data;
using System.Data;

namespace AjClipper.Tests
{
    [TestClass]
    public class DatabaseTests
    {
        private const string OleDbProviderFactoryName = "System.Data.OleDb";
        private const string OleDbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;";

        [TestMethod]
        public void CreateDatabase()
        {
            Database database = new Database("Test", OleDbProviderFactoryName, OleDbConnectionString);

            Assert.AreEqual("Test", database.Name);
            Assert.IsNotNull(database.ProviderFactory);
            Assert.IsInstanceOfType(database.ProviderFactory, typeof(System.Data.OleDb.OleDbFactory));
        }

        [TestMethod]
        public void GetConnection()
        {
            Database database = new Database("Test", OleDbProviderFactoryName, OleDbConnectionString);

            IDbConnection connection = database.GetConnection();

            Assert.IsNotNull(connection);
            Assert.IsInstanceOfType(connection, typeof(System.Data.OleDb.OleDbConnection));
            Assert.AreEqual(OleDbConnectionString, connection.ConnectionString);
        }
    }
}
