namespace AjSimpleData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Container
    {
        private Dictionary<string, Domain> domains = new Dictionary<string, Domain>();

        public Domain CreateDomain(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (domains.ContainsKey(name))
                throw new InvalidOperationException(string.Format("Domain '{0}' already exists", name));

            Domain domain = new Domain(name, this);
            domains[name] = domain;

            return domain;
        }

        public Domain GetDomain(string name)
        {
            if (!domains.ContainsKey(name))
                throw new InvalidOperationException(string.Format("Unknown Domain '{0}'", name));

            return domains[name];
        }
    }
}
