using System.ComponentModel;
using System.Windows.Automation.Peers;
using Commons.UI.WPF.TestUtils;
using NUnit.Framework;
using Commons.UI.LayoutDataStore;

namespace Commons.UI.WPF.LayoutDataStore.Test
{
    [TestFixture]
    public class WindowLayoutStoreTest : WindowTest<TestWindow>
    {
        private void testWindow_Closing(object sender, CancelEventArgs e)
        {
            if (worker != null) worker.Save();
        }

        protected TestWindow testWindow;
        protected ILayoutStoreWorker worker;

        private readonly ILayoutDataStorePathFactory defLayoutDataStorePathFactory
            = new LayoutDataStorePathFactory(LayoutDataTypePath.ExecutablePath);

        public ILayoutStoreWorker Worker
        {
            get { return worker; }
            set { worker = value; }
        }


        public override TestWindow CreateWindow()
        {
            testWindow = new TestWindow();
            testWindow.Title = "Test Window";
            testWindow.Name = "testWindow";
            return testWindow;
        }


        private void RunWorker()
        {
            worker = new LayoutStoreWorker(defLayoutDataStorePathFactory, testWindow.Name, new WindowLayoutStore(testWindow));
            worker.SaveDefault();
            worker.SetUI4LoadDefault();
            worker.Load();
        }

        [Test]
        [Explicit]
        public void ExplicitShow()
        {
            Show();
            RunApp();
        }

        [Test]
        public void Show()
        {
            RunWorker();
            testWindow.Closing += testWindow_Closing;
            ShowWin();
        }
    }
}
