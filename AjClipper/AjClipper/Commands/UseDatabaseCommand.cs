namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;
    using AjClipper.Data;

    public class UseDatabaseCommand : BaseCommand
    {
        private string name;
        private IExpression connectionExpression;
        private IExpression providerExpression;

        public UseDatabaseCommand(string name, IExpression connectionExpression, IExpression providerExpression)
        {
            this.name = name;
            this.connectionExpression = connectionExpression;
            this.providerExpression = providerExpression;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            string connectionString = (string) this.connectionExpression.Evaluate(environment);
            string providerName = (string) this.providerExpression.Evaluate(environment);

            Database database = new Database(this.name, providerName, connectionString);

            environment.SetPublicValue(this.name, database);
            environment.SetPublicValue(ValueEnvironment.CurrentDatabase, database);
        }
    }
}
