namespace Books.Inverse
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
                        Chapters = new List<Chapter>()
                    };

                    cookbook.Chapters.Add(new Chapter() { Title = "Models and Mappings", Book = cookbook, ChapterIndex = 0 });
                    cookbook.Chapters.Add(new Chapter() { Title = "Configuration and Schema", Book = cookbook, ChapterIndex = 1 });
                    cookbook.Chapters.Add(new Chapter() { Title = "Sessions and Transactions", Book = cookbook, ChapterIndex = 2 });

                    session.Save(cookbook);
                    tx.Commit();
                    session.Close();
                }
            }

            using (ISession session = sessionFactory.OpenSession())
            {
                foreach (Book book in session.Query<Book>().Fetch(b => b.Chapters))
                {
                    System.Console.WriteLine(string.Format("Book {0}", book.Title));
                    int nchapter = 0;
                    foreach (Chapter chapter in book.Chapters)
                    {
                        System.Console.WriteLine(string.Format("Chapter {0}:{1}", ++nchapter, chapter.Title));
                        System.Console.WriteLine(string.Format("From Book {0}", chapter.Book.Title));
                    }
                }

            }

            System.Console.ReadKey();
        }
    }
}
