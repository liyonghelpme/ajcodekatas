namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;
    using AjClipper.Data;
    using System.Data;

    public class UseWorkAreaCommand : BaseCommand
    {
        private IExpression nameExpression;
        private IExpression commandExpression;

        public UseWorkAreaCommand(IExpression nameExpression, IExpression commandExpression)
        {
            this.nameExpression = nameExpression;
            this.commandExpression = commandExpression;
        }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            string name = EvaluateUtilities.EvaluateAsName(this.nameExpression, environment);

            string commandText = null;
            
            if (this.commandExpression != null)
                commandText = (string)this.commandExpression.Evaluate(environment);

            Database database = (Database)environment.GetValue(ValueEnvironment.CurrentDatabase);

            WorkArea workarea;

            if (commandText != null)
            {
                IDbCommand command = database.ProviderFactory.CreateCommand();
                command.CommandText = commandText;
                command.Connection = database.GetConnection();
                workarea = new WorkArea(name, command, database.ProviderFactory);
            }
            else
                workarea = new WorkArea(name, database.GetConnection(), database.ProviderFactory);

            environment.SetPublicValue(name, workarea);
            environment.SetPublicValue(ValueEnvironment.CurrentWorkArea, workarea);
        }
    }
}
