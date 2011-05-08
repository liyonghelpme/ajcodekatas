namespace Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Chapter
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Notes { get; set; }
    }
}
