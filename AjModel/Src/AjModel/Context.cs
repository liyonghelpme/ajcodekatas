using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjModel
{
    public class Context
    {
        private IList<Repository> repositories = new List<Repository>();

        public static IContextProvider CurrentProvider { get; set; }

        public void AddRepository(Repository repository)
        {
            this.repositories.Add(repository);
        }

        public Repository GetRepository(string name)
        {
            return this.repositories.Where(r => r.EntityModel.Name == name).FirstOrDefault();
        }
    }
}
