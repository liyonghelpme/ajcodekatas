using System;
using System.Collections;

namespace AjAgents
{
	/// <summary>
	/// Summary description for Message.
	/// </summary>
	class Message
	{
		private IAgent sender;
		private string action;
		private object data;
		private IAgent receiver;

		public Message(IAgent sender, string action, object data)
		{
			this.sender = sender;
			this.action = action;
			this.data = data;
		}

		public Message(IAgent sender, IAgent receiver, string action, object data)
		{
			this.sender = sender;
			this.action = action;
			this.data = data;
			this.receiver = receiver;
		}

		public IAgent Sender 
		{
			get 
			{
				return sender;
			}
		}

		public IAgent Receiver
		{
			get 
			{
				return receiver;
			}
		}

		public string Action 
		{
			get 
			{
				return action;
			}
		}

		public object Data 
		{
			get 
			{
				return data;
			}
		}
	}
}
