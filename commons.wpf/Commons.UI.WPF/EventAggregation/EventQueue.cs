using System;
using System.Collections.Generic;

namespace Commons.UI.WPF.EventAggregation
{
	class EventQueue<T> : IEventQueue<T>
	{
		public List<T> subscriptions = new List<T>();

		public void Publish(object value)
		{
			Delegate action = subscriptions[0] as Delegate;
			action.DynamicInvoke(new object[] {value});
		}

		public void Subscribe(T action)
		{
			subscriptions.Add(action);
		}

		public object Key
		{
			get; set;
		}
	}
}
