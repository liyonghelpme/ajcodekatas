namespace AjClipper.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class WorkArea
    {
        private string name;
        private IDbConnection connection;
        private IDbCommand command;
        private System.Data.Common.DbProviderFactory factory;
        private DataTable dataTable;
        private DataRow currentRow;
        private int nrow = -1;

        public WorkArea(string name, IDbConnection connection, System.Data.Common.DbProviderFactory factory)
        {
            this.name = name;
            this.connection = connection;
            this.factory = factory;
            this.command = this.factory.CreateCommand();
            this.command.CommandText = "select * from " + this.Name;
            this.command.Connection = this.connection;
        }

        public WorkArea(string name, IDbCommand command, System.Data.Common.DbProviderFactory factory)
        {
            this.name = name;
            this.command = command;
            this.connection = this.command.Connection;
            this.factory = factory;
        }

        public string Name { get { return this.name; } }

        public bool ReadNext()
        {
            if (this.dataTable == null)
                this.ReadDataTable();

            nrow++;

            if (this.dataTable.Rows.Count <= nrow) {
                this.currentRow = null;
                return false;
            }

            this.currentRow = this.dataTable.Rows[nrow];

            return true;
        }

        public object GetField(string name)
        {
            return this.currentRow[name];
        }

        public void SetField(string name, object value)
        {
            this.currentRow[name] = value;
        }

        private void ReadDataTable()
        {
            IDbDataAdapter adapter = this.factory.CreateDataAdapter();
            adapter.SelectCommand = this.command;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            this.dataTable = ds.Tables[0];
        }
    }
}