using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjClipper.Data;

namespace AjClipper.Tests
{
    [TestClass]
    public class WorkAreaTests
    {
        [TestMethod]
        public void SqlClientCreateWorkArea()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=.\\TestData.mdf;Integrated Security=True");
            WorkArea workarea = new WorkArea("TestTable", conn, SqlClientFactory.Instance);

            Assert.AreEqual("TestTable", workarea.Name);
        }

        [TestMethod]
        [DeploymentItem("TestData.mdf")]
        [DeploymentItem("TestData_log.ldf")]
        [Ignore]
        public void SqlClientReadNext()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=.\\TestData.mdf;Integrated Security=True");
            WorkArea workarea = new WorkArea("TestTable", conn, SqlClientFactory.Instance);

            Assert.IsTrue(workarea.MoveNext());
        }

        [TestMethod]
        [DeploymentItem("Data\\TEST.DBF")]
        [DeploymentItem("Data\\TEST.CDX")]
        [DeploymentItem("Data\\TEST.DBT")]
        public void OleDbReadNext()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;");
            WorkArea workarea = new WorkArea("TEST", conn, OleDbFactory.Instance);

            Assert.IsTrue(workarea.MoveNext());
        }

        [TestMethod]
        [DeploymentItem("Data\\TEST.DBF")]
        [DeploymentItem("Data\\TEST.CDX")]
        [DeploymentItem("Data\\TEST.DBT")]
        public void OleDbReadNextUsingCommand()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;");
            OleDbCommand cmd = new OleDbCommand("select * from TEST order by CODIGO", conn);
            WorkArea workarea = new WorkArea("TEST", cmd, OleDbFactory.Instance);

            Assert.IsTrue(workarea.MoveNext());
        }

        [TestMethod]
        [DeploymentItem("Data\\TEST.DBF")]
        [DeploymentItem("Data\\TEST.CDX")]
        [DeploymentItem("Data\\TEST.DBT")]
        public void OleDbGetField()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;");
            WorkArea workarea = new WorkArea("TEST", conn, OleDbFactory.Instance);

            workarea.MoveNext();

            object result = workarea.GetField("CODIGO");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));
        }

        [TestMethod]
        [DeploymentItem("Data\\TEST.DBF")]
        [DeploymentItem("Data\\TEST.CDX")]
        [DeploymentItem("Data\\TEST.DBT")]
        public void OleDbGetFieldUsingCommand()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;");
            OleDbCommand cmd = new OleDbCommand("select * from TEST order by CODIGO", conn);
            WorkArea workarea = new WorkArea("TEST", cmd, OleDbFactory.Instance);

            workarea.MoveNext();

            object result = workarea.GetField("CODIGO");

            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        [DeploymentItem("Data\\TEST.DBF")]
        [DeploymentItem("Data\\TEST.CDX")]
        [DeploymentItem("Data\\TEST.DBT")]
        public void OleDbGetNameFieldUsingCommand()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.;Extended Properties=dBASE IV;User ID=Admin;Password=;");
            OleDbCommand cmd = new OleDbCommand("select * from TEST order by CODIGO", conn);
            WorkArea workarea = new WorkArea("TEST", cmd, OleDbFactory.Instance);

            workarea.MoveNext();

            object result = workarea.GetField("DETALLE");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual(result.ToString().Trim(), "uno");
        }
    }
}
