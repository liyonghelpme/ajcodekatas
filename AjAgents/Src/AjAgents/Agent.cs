using System;

namespace AjAgents
{
	public class Agent : IAgent
	{
		private IAgentHost host;

		public Agent()
		{
		}

		public void Publish(string action, object data) 
		{
			if (host!=null)
				host.Publish(this,action,data);
		}

		public void Subscribe(string action) 
		{
			if (host!=null)
				host.Subscribe(this,action);
		}

		#region IAgent Members

		public virtual void Start()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void Process(IAgent sender, string action, object data)
		{
			
		}

		public virtual IAgentHost Host
		{
			get
			{
				return host;
			}
			set
			{
				host = value;
			}
		}

		#endregion
	}
}
