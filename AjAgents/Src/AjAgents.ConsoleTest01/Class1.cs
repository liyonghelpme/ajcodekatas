using System;

using AjAgents;

namespace AjAgents.ConsoleTest01
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			AgentHost host = new AgentHost();
			Agent agent1 = new AgentTest("Agent 1");
			Agent agent2 = new AgentTest("Agent 2");
			host.Start(agent1);
			host.Start(agent2);
			host.Publish(null,"Message","Message 1");
			host.Publish(null,"Message","Message 2");
			host.Publish(null,"Message.Agent 1","Message for Agent 1");
			host.Publish(null,"Message.Agent 2","Message for Agent 2");
			Console.ReadLine();
			host.Stop(agent1);
			host.Stop(agent2);
			Console.ReadLine();
		}
	}
}

	