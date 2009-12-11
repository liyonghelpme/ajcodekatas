namespace AjClipper.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjClipper.Expressions;

    public class LocalCommand : BaseCommand
    {
        private ICollection<string> names;

        public LocalCommand(ICollection<string> names)
        {
            this.names = names;
        }

        public ICollection<string> Names { get { return this.names; } }

        public override void Execute(Machine machine, ValueEnvironment environment)
        {
            ValueEnvironment localenv = environment.GetLocalEnvironment();

            foreach (string name in this.names)
                if (localenv.GetValue(name) == null)
                    localenv.SetEnvironmentValue(name, null);
        }
    }
}
