using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjModel.WebMvc.Models
{
    public class ContextProvider : IContextProvider
    {
        private static Context context;

        static ContextProvider()
        {
            context = new Context();
            context.AddRepository(new Repository<Customer>(ModelProvider.Instance.GetEntityModel<Customer>(), Domain.Instance.Customers));
            context.AddRepository(new Repository<Person>(ModelProvider.Instance.GetEntityModel<Person>(), Domain.Instance.Persons));
        }

        public Context GetInstance()
        {
            return context;
        }
    }
}
