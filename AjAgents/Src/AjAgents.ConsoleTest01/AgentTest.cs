using System;
using AjAgents;

namespace AjAgents.ConsoleTest01
{
	public class AgentTest : Agent
	{
		string name;

		public AgentTest(string name)
		{
			this.name = name;
		}

		public override void Start() 
		{
			Console.WriteLine(String.Format("Agent {0}: Start",name));
			Subscribe("Message");
			Subscribe("Message."+name);
		}

		public override void Stop() 
		{
			Console.WriteLine(String.Format("Agent {0}: Stop",name));
		}

		public override void Process(IAgent sender, string action, object obj) 
		{
			Console.WriteLine(String.Format("Agent {0}: Process Action {1}: {2}",name,action,obj.ToString()));
		}
	}
}

