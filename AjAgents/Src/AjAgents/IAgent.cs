using System;

namespace AjAgents
{
	public interface IAgent
	{
		void Start();
		void Stop();
		void Process(IAgent sender, string action, object data);
		IAgentHost Host { get; set; }
	}
}
