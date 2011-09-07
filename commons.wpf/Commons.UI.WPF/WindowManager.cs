using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF.Controls;
using Commons.UI.WPF.LayoutDataStore;
using Commons.UI.WPF.Properties;

namespace Commons.UI.WPF
{
    public class WindowManager : IWindowManager
    {
        private readonly AbstractMainWindow mainWindow;
        private DialogLayoutStoreWorkerContainer container;
    	private ILayoutDataStorePathFactory layoutDataStorePathFactory;

    	public WindowManager(AbstractMainWindow mainWindow, ILayoutDataStorePathFactory layoutDataStorePathFactory)
    	{
    		this.mainWindow = mainWindow;
    		this.layoutDataStorePathFactory = layoutDataStorePathFactory;
    	}

    	#region IWindowManager Members

        public void AddMainPane(Control control, object caption, object icon)
        {
            if (icon is string)
                icon = new Image()
                           {
                               Source = new BitmapImage(new Uri((string) icon, UriKind.Relative)),
                               Height = 16,
                               Width = 16,
                               Margin = new Thickness(5, 0, 0, 0)
                           };

        	IContainsLayoutStores containsLayoutStores = control as IContainsLayoutStores;
        	Action closeAction = null;
        	if(containsLayoutStores!=null)
			{
				LayoutStoreWorkerContainer workerContainer = new LayoutStoreWorkerContainer(control.Name, containsLayoutStores.GetLayoutStores(), layoutDataStorePathFactory);
				closeAction = () => workerContainer.Close();
			}

        	mainWindow.AddTabPane(control, caption, icon, closeAction);
        }

        public bool ShowDialog(Control view, string title)
        {
            return ShowDialog(view, title, true);
        }

        /// <summary>
        /// show view as dialog
        /// </summary>
        /// <param name="view"></param>
        /// <param name="caption"></param>
        /// <param name="showOkCancelPanel"></param>
        /// <returns>true if DialogResult.OK</returns>
        public bool ShowDialog(object view, string caption, bool showOkCancelPanel)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (!(view is Control)) throw new ArgumentException("view type not supported by this WindowManager");

			Control control = (Control) view;

            OKCancelControlContainer window = GetOkCancelWindow(control, caption, showOkCancelPanel);
            GetContainer(control, window);

            using (container)
            {
            	var result = (bool) window.ShowDialog();

				//detach control from container, otherwise we can't reuse it
            	window.Control = null;
            	return result;
            }
        }


        public bool Ask(string text, string header)
        {
            if (mainWindow != null)
                return MessageBox.Show(mainWindow, text, header, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                       MessageBoxResult.Yes;
            else
                return MessageBox.Show(text, header, MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                       MessageBoxResult.Yes;
        }

        public void Error(string errorMessage)
        {
            string error = Resources.Error;
            if (mainWindow != null)
                MessageBox.Show(mainWindow, errorMessage, error, MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(errorMessage, error, MessageBoxButton.OK, MessageBoxImage.Error);
        }

    	public void ShowWindow(Window window)
    	{
    		var containsLayoutStores = window as IContainsLayoutStores;
    		IList<ILayoutDataStore> layoutDataStores = new List<ILayoutDataStore>();

			if (containsLayoutStores != null)
				layoutDataStores = containsLayoutStores.GetLayoutStores();

			if (string.IsNullOrEmpty(window.Name))
			{
				var type = window.GetType();
				//???? ??????? ???????????, ? Name ?? ????? - ?? ??????????? ??? ????
				window.Name = type.Namespace.StartsWith("System.") ? null : type.Name;
			}

			if (string.IsNullOrEmpty(window.Name))
				throw new ArgumentException(Resources.ControlNameCanNotBeEmpty);

    		var worker = new LayoutStoreWorker(layoutDataStorePathFactory, window.Name, layoutDataStores);
			LayoutStoreSupportUtils.Load(worker);
			window.Closing += (s, e) => LayoutStoreSupportUtils.Close(worker);

			window.Show();
    	}

    	public void Show(Control view, string title)
        {
            OKCancelControlContainer window = GetOkCancelWindow(view, title, false);
            GetContainer(view, window);
			window.Closed += (s, e) => container.Close();
            window.Show();
        }

        #endregion

        private OKCancelControlContainer GetOkCancelWindow(Control control, string caption, bool showOkCancelPanel)
        {
			//TODO showOkCancelPanel to constructor?
            OKCancelControlContainer window = new OKCancelControlContainer(control, caption);

            window.WindowStartupLocation = WindowStartupLocation.Manual;

        	window.ShowInTaskbar = false;

            window.OKCancelPanelVisible = showOkCancelPanel;
            if (mainWindow != null)
            {
                window.Owner = mainWindow;
                window.Icon = mainWindow.Icon;
            }
            return window;
        }

        private DialogLayoutStoreWorkerContainer GetContainer(Control control, OKCancelControlContainer window)
        {
			IList<ILayoutDataStore> stores = null;
        	
        	if (control is IContainsLayoutStores)
				stores = ((IContainsLayoutStores)control).GetLayoutStores();

            container = new DialogLayoutStoreWorkerContainer(window,
            	                                                 stores, layoutDataStorePathFactory);
        	return container;
        }
    }
}
