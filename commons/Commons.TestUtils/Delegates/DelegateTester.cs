using System;
using System.Threading;
using NUnit.Framework;


namespace Commons.TestUtils
{
	public class DelegateTester : IDisposable
	{
		private int? mustBeCalledCount;
		protected int calledCount;

		public readonly object locker = new object();

		private ManualResetEvent waitHandle;

		public DelegateTester():this(true)
		{
		}

		public DelegateTester(bool mustBeCalledOnce):this(mustBeCalledOnce ? (int?)1 : null)
		{
		}

		public DelegateTester(int? mustBeCalledCount)
		{
			this.mustBeCalledCount = mustBeCalledCount;

			waitHandle = new ManualResetEvent(false);
		}

		public virtual void Dispose()
		{
			lock (locker)
			{
				int? expected = mustBeCalledCount ?? 0;
				Assert.That(calledCount, 
					Is.EqualTo(expected), 
					"delegate wasn't called needed times or error on custom checks");
			}
		}

		public void OnCall<TP>(TP value)
		{
			lock (locker)
			{
				InternalOnCall(value);
			}
			
		}

		protected virtual void InternalOnCall(object value)
		{
			if (mustBeCalledCount!=null && ++calledCount > mustBeCalledCount)
				throw new CalledMoreTimesThanNeededException(mustBeCalledCount);

			waitHandle.Set();
		}

		public void Wait(int millisecondsTimeout)
		{
			waitHandle.WaitOne(millisecondsTimeout);
		}
	}

	public class CustomDelegateTester<T>:DelegateTester
	{
		private readonly Action<T> customCheck;

		public CustomDelegateTester(Action<T> customCheck, bool mustBeCalledOnce) : base(mustBeCalledOnce)
		{
			this.customCheck = customCheck;
		}

		protected override void InternalOnCall(object value)
		{
			customCheck.Invoke((T) value);
			base.InternalOnCall(value);
		}
	}

	/// <summary>
	/// delegate tester with return value
	/// </summary>
	/// <typeparam name="R">return value type</typeparam>
	/// <typeparam name="T"></typeparam>
	public class DelegateTester<R> : DelegateTester
	{
		private R r;

		public DelegateTester(R r)
		{
			this.r = r;
		}

		public R OnCall<TP>(TP value)
		{
			base.OnCall(value);

			return r;
		}
	}
}
