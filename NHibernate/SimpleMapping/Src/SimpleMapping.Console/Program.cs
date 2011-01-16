namespace SimpleMapping.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NHibernate;
    using NHibernate.Cfg;
    using SimpleMapping.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Customer customer1 = new Customer();
                    customer1.Name = "Customer 1";
                    customer1.Address = "Address 1";
                    customer1.Notes = "Notes 1";
                    session.Save(customer1);

                    IQuery query = session.CreateQuery("from Customer");
                    
                    foreach (Customer c in query.List<Customer>())
                        System.Console.WriteLine(string.Format("Customer {0}", c.Name));

                    tx.Commit();
                    session.Close();
                }
            }

            System.Console.ReadKey();
        }
    }
}
