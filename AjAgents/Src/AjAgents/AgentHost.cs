using System;
using System.Collections;
using System.Threading;

namespace AjAgents
{
	public class AgentHost : IAgentHost
	{
		private IList agents;
		private IDictionary subscriptions;

		public AgentHost()
		{
			agents = new ArrayList();
			subscriptions = new Hashtable();
		}

		#region IAgentHost Members

		public void Start(IAgent agent)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(StartAgent), agent);
		}

		private void StartAgent(object obj) 
		{
			IAgent agent = (IAgent) obj;

			lock (agent) 
			{
				agent.Host=this;
				agent.Start();
				agents.Add(agent);
			}
		}

		public void Stop(IAgent agent)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(StopAgent), agent);
		}

		private void StopAgent(Object obj)
		{
			IAgent agent = (IAgent) obj;

			lock (agent) 
			{
				agents.Remove(agent);
				agent.Stop();
				agent.Host=null;
			}
		}

		public void Publish(IAgent sender, string action, object data)
		{
			Message msg = new Message(sender,action,data);
			ThreadPool.QueueUserWorkItem(new WaitCallback(BroadcastMessage),msg);
		}

		public void Send(IAgent sender, IAgent receiver, string action, object data) 
		{
			Message msg = new Message(sender,receiver,action,data);
			ThreadPool.QueueUserWorkItem(new WaitCallback(SendMessage),msg);
		}

		private void SendMessage(object obj) 
		{
			Message msg = (Message) obj;
			lock (msg.Receiver) 
			{
				msg.Receiver.Process(msg.Sender,msg.Action,msg.Data);
			}
		}

		private void BroadcastMessage(object obj) 
		{
			Message msg = (Message) obj;

			IList subscribers = (IList) subscriptions[msg.Action];

			if (subscribers==null)
				return;

			foreach (IAgent agent in subscribers)
				Send(msg.Sender,agent,msg.Action,msg.Data);
		}

		public void Subscribe(IAgent subscriber, string actionname)
		{
			if (!subscriptions.Contains(actionname))
				subscriptions[actionname]=new ArrayList();

			((IList) subscriptions[actionname]).Add(subscriber);
		}

		#endregion
	}
}
