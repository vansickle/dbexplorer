namespace Commons.UI.WPF.EventAggregation
{
	public interface IEventQueue<T>:IEventQueue
	{
		void Publish(object value);
		void Subscribe(T action);
	}

	public interface IEventQueue
	{
		object Key { get; set; }
	}
}
