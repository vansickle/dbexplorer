using System.Windows;
using NUnit.Framework;

namespace Commons.UI.WPF.TestUtils
{
    public abstract class ViewTest<T> where T : Window
    {
        protected T window;

        public T Window
        {
            get { return window; }
        }

        protected void RunApp()
        {
            Application application = new Application();
            application.Run();
        }

        [TearDown]
        public virtual void TearDown()
        {
            window.Close();
        }

        protected void ShowWin()
        {
            Window.Show();
        }
    }
}
