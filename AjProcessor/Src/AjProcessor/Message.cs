namespace AjProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Message
    {
        public Message()
        {
            this.Properties = new Properties();
        }

        public Message(object payload)
            : this()
        {
            this.Payload = payload;
        }

        public Message(object payload, string action)
            : this()
        {
            this.Payload = payload;
            this.Action = action;
        }

        public Message(object payload, string action, string to)
            : this()
        {
            this.Payload = payload;
            this.Action = action;
            this.To = to;
        }

        public object Payload { get; set; }
        public Properties Properties { get; set; }

        public string To
        {
            get
            {
                return (string) this.Properties["To"];
            }

            set
            {
                this.Properties["To"] = value;
            }
        }

        public string Action
        {
            get
            {
                return (string) this.Properties["Action"];
            }

            set
            {
                this.Properties["Action"] = value;
            }
        }
    }
}
