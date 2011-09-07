using NUnit.Framework;


namespace Commons.TestUtils
{
	public class DelegateNotCalledTester:DelegateTester
	{
		public DelegateNotCalledTester() : base(0)
		{
		}

//		public override void Dispose()
//		{
//			Assert.That(calledCount, 
//					Is.EqualTo(0), 
//					"delegate wasn't called");
//		}
	}
}
