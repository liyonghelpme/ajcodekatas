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
        private IExpression nameExpression;
        private IExpression connectionExpression;
        private IExpression providerExpression;

        public UseDatabaseCommand(IExpression nameExpression, IExpression connectionExpression, IExpression providerExpression)
        {
            this.nameExpression = nameExpression;
            this.connectionExpression = connectionExpression;
            this.providerExpression = providerExpression;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            string name = EvaluateUtilities.EvaluateAsName(this.nameExpression, environment);

            string connectionString = (string) this.connectionExpression.Evaluate(environment);
            string providerName = (string) this.providerExpression.Evaluate(environment);

            Database database = new Database(name, providerName, connectionString);

            environment.SetPublicValue(name, database);
            environment.SetPublicValue(ValueEnvironment.CurrentDatabase, database);
        }
    }
}
