namespace AjTwitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Message
    {
        public Message(User user, string text)
            : this(user, text, new DateTime())
        {
        }

        public Message(User user, string text, DateTime dateTime)
        {
            this.User = user;
            this.Text = text;
            this.DateTime = dateTime;
        }

        public User User { get; private set; }

        public string Text { get; private set; }

        public DateTime DateTime { get; private set; }
    }
}
