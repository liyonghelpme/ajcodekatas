namespace AjTwitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class User
    {
        private UserList followers = new UserList();
        private UserList following = new UserList();
        private MessageList messages = new MessageList();

        public User(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public string Bio { get; set; }

        public IEnumerable<Message> Messages
        {
            get
            {
                return this.messages.Elements;
            }
        }

        public int MessagesCount
        {
            get
            {
                return this.messages.Count;
            }
        }

        public int FollowersCount
        {
            get
            {
                return this.followers.Count;
            }
        }

        public int FollowingCount
        {
            get
            {
                return this.following.Count;
            }
        }

        public IEnumerable<User> Followers
        {
            get
            {
                return this.followers.Elements;
            }
        }

        public IEnumerable<User> Following
        {
            get
            {
                return this.following.Elements;
            }
        }

        public void NewMessage(string text)
        {
            Message message = new Message(this, text);

            this.messages.Add(message);
        }

        public void AddFollower(User user)
        {
            this.followers.Add(user);
            user.following.Add(this);
        }

        public void RemoveFollower(User user)
        {
            this.followers.Remove(user);
            user.following.Remove(this);
        }

        public IEnumerable<Message> GetMessages(int count)
        {
            Message[] messages = new Message[count];

            IEnumerator<Message>[] lists = new IEnumerator<Message>[1 + this.FollowingCount];

            int n = 0;

            lists[n++] = this.Messages.GetEnumerator();

            foreach (User following in this.Following)
                if (n < lists.Length)
                    lists[n++] = following.Messages.GetEnumerator();

            int nmsgs = 0;

            foreach (IEnumerator<Message> enumerator in lists)
                if (enumerator != null)
                    while (enumerator.MoveNext() && InsertMessage(messages, enumerator.Current))
                    {
                        nmsgs++;
                    }

            if (nmsgs < count)
                Array.Resize(ref messages, nmsgs);

            return messages;
        }

        private static bool InsertMessage(Message[] messages, Message message)
        {
            for (int k = 0; k < messages.Length; k++)
                if (messages[k] == null)
                {
                    messages[k] = message;
                    return true;
                }
                else if (message.DateTime < messages[k].DateTime)
                {
                    for (int j = messages.Length - 1; j > k; j--)
                        messages[j] = messages[j - 1];

                    messages[k] = message;

                    return true;
                }

            return false;
        }
    }
}

