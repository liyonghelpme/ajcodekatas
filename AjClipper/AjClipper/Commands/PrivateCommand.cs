namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class PrivateCommand : BaseCommand
    {
        private ICollection<string> names;

        public PrivateCommand(ICollection<string> names)
        {
            this.names = names;
        }

        public ICollection<string> Names { get { return this.names; } }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            ValueEnvironment privateenv = environment.GetNormalEnvironment();

            foreach (string name in this.names)
                if (privateenv.GetValue(name) == null)
                    privateenv.SetEnvironmentValue(name, null);
        }
    }
}
