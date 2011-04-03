namespace ItemsTablePerHierarchyMapping.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NHibernate;
    using NHibernate.Cfg;
    using ItemsTablePerHierarchyMapping.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Page page1 = new Page();
                    page1.Title = "Technical Blob";
                    page1.Description = "My Personal Technical Blob in English";
                    page1.Url = "http://ajlopez.wordpress.com";

                    Page page2 = new Page();
                    page2.Title = "My Personal Blob";
                    page2.Description = "My Personal Blob in Spanish";
                    page2.Url = "http://ajlopez.zoomblog.com";

                    Note note1 = new Note();
                    note1.Title = "To Do";
                    note1.Description = "My To Do List";
                    note1.Content = "Practice NHibernate";

                    session.Save(page1);
                    session.Save(page2);
                    session.Save(note1);

                    IQuery query = session.CreateQuery("from Item");

                    foreach (Item item in query.List<Item>())
                        System.Console.WriteLine(string.Format("Item {0}", item.Title));

                    tx.Commit();
                    session.Close();
                }
            }

            System.Console.ReadKey();
        }
    }
}
