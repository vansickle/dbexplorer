using System.Windows;
using NUnit.Framework;

namespace Commons.UI.WPF.TestUtils
{
	public abstract class WindowTest<T> : ViewTest<T> where T:Window
	{
		public abstract T CreateWindow();

		[SetUp]
		public void SetUp()
		{
			window = CreateWindow();
		}
	}
    public class UITest
    {
        protected void RunApp()
        {
            Application application = new Application();
            application.Run();
        }
    }

}
