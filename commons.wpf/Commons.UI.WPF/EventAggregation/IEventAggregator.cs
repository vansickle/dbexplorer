namespace Commons.UI.WPF.EventAggregation
{
	public interface IEventAggregator
	{
		IEventQueue<T> Get<T>();
	}
}
