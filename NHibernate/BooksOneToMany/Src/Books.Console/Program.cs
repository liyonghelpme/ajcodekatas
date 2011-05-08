namespace ItemsTablePerHierarchyMapping.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Linq;
    using Books;

    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Book cookbook = new Book()
                    {
                        Title = "NHibernate Cookbook",
                        Author = "Jason Dentler",
                        //Chapters = new List<Chapter>() { 
                        //    new Chapter() { Title = "Models and Mappings" },
                        //    new Chapter() { Title = "Configuration and Schema" },
                        //    new Chapter() { Title = "Sessions and Transactions" }
                        //}
                    };

                    cookbook.Chapters = new List<Chapter>();
                    cookbook.Chapters.Add(new Chapter() { Title = "Models and Mappings" }); 

                    session.Save(cookbook);

                    foreach (Book book in session.Query<Book>())
                    {
                        System.Console.WriteLine(string.Format("Book {0}", book.Title));
                        int nchapter = 0;
                        foreach (Chapter chapter in book.Chapters)
                            System.Console.Write(string.Format("Chapter {0}:{1}", ++nchapter, chapter.Title));
                    }

                    tx.Commit();
                    session.Close();
                }
            }

            System.Console.ReadKey();
        }
    }
}
