using System;

namespace Commons.TestUtils
{
	public class CalledMoreTimesThanNeededException : ApplicationException
	{
		public CalledMoreTimesThanNeededException(int? count) : base("delegate must be called only :"+count)
		{
		}
	}
}
