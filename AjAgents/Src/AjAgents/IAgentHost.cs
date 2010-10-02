using System;

namespace AjAgents
{
	public interface IAgentHost
	{
		void Start(IAgent agent);
		void Stop(IAgent agent);
		void Publish(IAgent sender, string action, object data);
		void Send(IAgent sender, IAgent receiver, string action, object data);
		void Subscribe(IAgent subscriber, string action);
	}
}
