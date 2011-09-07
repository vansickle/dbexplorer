using System;
using System.Collections;

namespace Commons.Data.DemoData
{
	/// <summary>
	/// for make bulk operations on joined lists
	/// </summary>
	public class Joiner
	{
		private readonly IList[] lists;

		public Joiner(params IList[] lists)
		{
			this.lists = lists;
		}

		public void ForEach(Action<object> action)
		{
			foreach (var list in lists)
			{
				foreach (var item in list)
				{
					action.Invoke(item);
				}
			}
		}
	}
}
