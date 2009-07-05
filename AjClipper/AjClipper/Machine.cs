namespace AjClipper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Machine
    {
        private ValueEnvironment environment;

        public Machine()
            : this(new ValueEnvironment())
        {
        }

        public Machine(ValueEnvironment environment)
        {
            this.environment = environment;
        }

        public ValueEnvironment Environment
        {
            get
            {
                return this.environment;
            }
        }
    }
}
