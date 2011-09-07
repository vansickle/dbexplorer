using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Db4oExplorer.Domain;
using Db4oExplorer.Explorer.Filters;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Explorer
{
	/// <summary>
	/// Interaction logic for DbExplorer.xaml
	/// </summary>
	public partial class DbExplorer : UserControl
	{
		public DbExplorer()
		{
			InitializeComponent();
		}

		public ExplorerViewModel DataSource
		{
			get { return DataContext as ExplorerViewModel; }
			set
			{
				DataContext = value;

				foreach (var filter in value.Filters)
				{
					var item = new MenuItem()
					           	{
					           		IsCheckable = true, 
					           		Header = filter.ActionName, 
					           		IsChecked = filter.IsEnabled
					           	};
					IStoredClassFilter classFilter = filter;
					item.Click += 
						(sender, args) =>
							{
								classFilter.IsEnabled = !classFilter.IsEnabled; 
								treeView.Items.Refresh();
							};

					filtersMenu.Items.Add(
						item);
				}
			}
		}

		public event Action<IConnection> EditConnectionFired;

		private void editConnectionMenuItem_Click(object sender, RoutedEventArgs e)
		{
			var model = (ConnectionViewModel)treeView.SelectedItem;
			EditConnectionFired.Fire(model.Connection);
		}

		public event Action<IConnection> DeleteConnectionFired;

		private void deleteConnectionMenuItem_Click(object sender, RoutedEventArgs e)
		{
			var model = (ConnectionViewModel)treeView.SelectedItem;
			DeleteConnectionFired.Fire(model.Connection);
		}

		public event Action<IStoredClass> ShowFieldsFired;

		private void showFieldsMenuItem_Click(object sender, RoutedEventArgs e)
		{
			IStoredClass obj = GetStoredClass(sender);
			ShowFieldsFired.Fire(obj);
		}

		private IStoredClass GetStoredClass(object sender)
		{
			MenuItem item = (MenuItem)sender;
			return (IStoredClass)item.DataContext;
		}

		private Field GetField(object sender)
		{
			MenuItem item = (MenuItem)sender;
			return (Field)item.DataContext;
		}

		private void classStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				StackPanel item = (StackPanel)sender;
				IStoredClass obj = (IStoredClass)item.DataContext;
				ShowDataFired.Fire(obj);
				e.Handled = true;
			}
		}


		public event Action<IStoredClass> ShowDataFired;

		private void showDataMenuItem_Click(object sender, RoutedEventArgs e)
		{
			if (ShowDataFired != null)
				ShowDataFired.Invoke((IStoredClass)treeView.SelectedItem);
		}

		private void switchClassNameType_Click(object sender, RoutedEventArgs e)
		{
			Switch();
			//			bool content = classItemTemplate.HasContent;
			//			classItemTextBlock.SetBinding(TextBlock.TextProperty, binding);
			//			classItemTextBlock.Text = binding;
		}

		private void Switch()
		{
			Binding binding = new Binding("Name");
			Binding pureNamebinding = new Binding("PureName");
			//			HierarchicalDataTemplate template = (HierarchicalDataTemplate) this.FindResource("classItemTemplate");
			//			HierarchicalDataTemplate explorerTemplate = (HierarchicalDataTemplate) treeView.ItemTemplate;
			//			template = (HierarchicalDataTemplate) template.ItemTemplate;

			HierarchicalDataTemplate template = new HierarchicalDataTemplate();
			template.ItemsSource = new Binding("Objects");
			//			FrameworkElementFactory factory = template.VisualTree;
			FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
			factory.SetBinding(TextBlock.TextProperty, binding);
			template.VisualTree = factory;

			HierarchicalDataTemplate classTemplate = new HierarchicalDataTemplate();
			FrameworkElementFactory classfactory = new FrameworkElementFactory(typeof(TextBlock));
			classfactory.SetBinding(TextBlock.TextProperty, pureNamebinding);
			classTemplate.VisualTree = classfactory;

			template.ItemTemplate = classTemplate;

			treeView.ItemTemplate = template;
		}

		public event Action<Field> RenameFieldFired;

		private void renameFieldMenuItem_Click(object sender, RoutedEventArgs e)
		{
			RenameFieldFired.Fire(GetField(sender));
		}

		public event Action<IStoredClass> RenameClassFired;

		private void renameClassMenuItem_Click(object sender, RoutedEventArgs e)
		{
			RenameClassFired.Fire(GetStoredClass(sender));
		}

		private void sortBy_Click(object sender, RoutedEventArgs e)
		{
			StoredClassSortConverter converter = (StoredClassSortConverter) FindResource("storedClassSortConverter");
			converter.Direction = SortDirection.ASC;
			treeView.Items.Refresh();
		}

		private void collapseButton_Click(object sender, RoutedEventArgs e)
		{
			SwitchButtonPanel();
		}

		private void SwitchButtonPanel()
		{
			buttonPanel.Visibility = (buttonPanel.Visibility == Visibility.Collapsed)
			                         	? Visibility.Visible
			                         	: Visibility.Collapsed;
		}

		private void connectionStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				StackPanel item = (StackPanel)sender;
				ConnectionViewModel connectionViewModel = (ConnectionViewModel)item.DataContext;
				if (connectionViewModel.IsConnected) return;
				connectionViewModel.Connect();
			}
		}

		public event Action<IConnection> ShowStatisticsFired;

		private void showStatisticsMenuItem_Click(object sender, RoutedEventArgs e)
		{
			IConnection connection = GetConnectionFromMenuItem(sender);
			ShowStatisticsFired.Fire(connection);
		}

		private IConnection GetConnectionFromMenuItem(object sender)
		{
			MenuItem item = (MenuItem)sender;
			var connectionViewModel = (ConnectionViewModel)item.DataContext;
			return connectionViewModel.Connection;
		}

		public void FocusSearch()
		{
			Focus();
			SwitchButtonPanel();
			filterBox.Focus();
		}

		public event Action<IConnection> CreateNewStoredClassFired;

		private void createNewStoredClassMenuItem_Click(object sender, RoutedEventArgs e)
		{
			CreateNewStoredClassFired.Fire(GetConnectionFromMenuItem(sender));
		}

		public event Action<IStoredClass> ShowQueryToolFired;

		private void showQueryToolMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ShowQueryToolFired.Fire(GetStoredClass(sender));
		}

		public event Action<string> AddNewConnectionFired;

		private void treeView_Drop(object sender, DragEventArgs e)
		{
			if(!e.Data.GetDataPresent(DataFormats.FileDrop))
				return;

			string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];

			AddNewConnectionFired.Fire(data[0]);

			InvalidateVisual();
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			if(DataSource.IsEmpty)
			{
				var text = new FormattedText("Drag'n'drop your db here",CultureInfo.InvariantCulture,FlowDirection,new Typeface("Verdana"), 12, Brushes.DarkGray);
				var x = (ActualWidth - text.Width)/2;
				var y = (ActualHeight - text.Height)/2;
				drawingContext.DrawText(
					text, 
					new Point(x,y));
			}
			base.OnRender(drawingContext);
		}
	}

	public enum SortDirection
	{
		ASC = 1,
		DESC = -1
	}
}
