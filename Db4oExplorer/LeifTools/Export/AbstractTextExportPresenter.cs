using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Db4oExplorer;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer;
using Commons.UI.WPF;
using Commons.UI.WPF.Services;

namespace LeifTools.Export
{
	public class AbstractTextExportPresenter
	{
		private readonly IWindowManager windowManager;
		private readonly IFileManager fileManager;
		private ObservableCollection<ConnectionViewModel> connectionViewModels;
		private ITextExporter exporter;
		private string defaultExt;

		public AbstractTextExportPresenter(MainWindow mainWindow, IWindowManager windowManager, IFileManager fileManager, ITextExporter exporter, string defaultExt)
		{
			this.windowManager = windowManager;
			this.fileManager = fileManager;
			this.exporter = exporter;
			connectionViewModels = mainWindow.DataSource.Connections;
			this.defaultExt = defaultExt;
		}

		public void Export()
		{
			
			var connectionViewModel = connectionViewModels.First(cn => cn.IsConnected);

			var comboBox = new ComboBox(){Name = "classSelector", Height = 18, MinHeight = 18};
			comboBox.ItemsSource = connectionViewModel.Objects;
			windowManager.ShowDialog(comboBox, "Select class");

			IStoredClass storedClass = (IStoredClass) comboBox.SelectedValue;

			var dbObjects = storedClass.GetData();
			var export = exporter.Export(dbObjects);

			var resultView = new ExportResultView();
			resultView.SaveFired += () => fileManager.Save(resultView.Text, new BrowseParams()
			                                                                	{
			                                                                		DefaultExt = defaultExt, 
			                                                                		FileNameWithoutExt = storedClass.PureName,
			                                                                		Filter = String.Format(".{0}|*.{0}",defaultExt)
			                                                                	});
			resultView.Text = export;

			windowManager.AddMainPane(resultView, defaultExt.ToUpper() + " export: "+storedClass.PureName, null);
		}
	}
}
