namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class PublicCommand : BaseCommand
    {
        private ICollection<string> names;

        public PublicCommand(ICollection<string> names)
        {
            this.names = names;
        }

        public ICollection<string> Names { get { return this.names; } }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            ValueEnvironment pubenv = environment.GetPublicEnvironment();

            foreach (string name in this.names)
                if (pubenv.GetValue(name) == null)
                    environment.SetPublicValue(name, null);
        }
    }
}
