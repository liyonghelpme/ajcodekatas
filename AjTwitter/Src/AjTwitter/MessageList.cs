namespace AjTwitter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class MessageList : ObjectList<Message>
    {
        public override IEnumerable<Message> Elements
        {
            get
            {
                for (ObjectListNode<Message> node = this.FirstNode; node != null; node = node.Next)
                {
                    for (int k = node.Elements.Length; k-- > 0;)
                    {
                        Message element = node.Elements[k];

                        if (element != null)
                            yield return element;
                    }
                }
            }
        }

        public override void Add(Message element)
        {
            this.AddFirst(element);
        }
    }
}

