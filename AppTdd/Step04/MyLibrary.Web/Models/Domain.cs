using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibrary.Web.Models
{
    public class Domain
    {
        private static Domain instance = new Domain();

        private Domain()
        {
            this.Subjects = new List<Subject>()
            {
                new Subject() { Id = 1, Name = "Mathematics" },
                new Subject() { Id = 2, Name = "Physics" },
                new Subject() { Id = 3, Name = "Biology" },
                new Subject() { Id = 4, Name = "Literature" }
            };
        }

        public static Domain Instance { get { return instance; } }

        public IList<Subject> Subjects { get; set; }
    }
}
