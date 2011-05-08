namespace Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Book
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual IList<Chapter> Chapters { get; set; }
    }
}
