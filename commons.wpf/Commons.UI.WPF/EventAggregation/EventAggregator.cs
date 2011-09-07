using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Commons.UI.WPF.EventAggregation
{
	public class EventAggregator:IEventAggregator
	{
		private readonly List<IEventQueue> queues = new List<IEventQueue>();

		public IEventQueue<TEvent> Get<TEvent>()
		{
			IEventQueue<TEvent> queue = queues.FirstOrDefault(q => q.Key == typeof (TEvent)) as IEventQueue<TEvent>;
			if (queue == null)
			{
				queue = Activator.CreateInstance<EventQueue<TEvent>>();
				queue.Key = typeof (TEvent);
				queues.Add(queue);
			}
			return queue;
		}
	}
}
