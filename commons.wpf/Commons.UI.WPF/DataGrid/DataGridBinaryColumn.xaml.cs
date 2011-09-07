using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
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
using Microsoft.Windows.Controls;
using Commons.UI.WPF.EventWiring;

namespace Commons.UI.WPF.DataGrid
{
	/// <summary>
	/// Interaction logic for DataGridBinaryColumn.xaml
	/// 
	/// </summary>
	public partial class DataGridBinaryColumn : DataGridTemplateColumn,IBindableDataGridColumn
	{
		private string bindingPath;

		public DataGridBinaryColumn()
		{
			InitializeComponent();
		}

		public event Func<object, string, object, bool> ShowBinaryViewerFired;

		private void ShowBinaryViewer(object sender, RoutedEventArgs e)
		{
			var window = new Window();
			Button button = (Button) sender;
			object rowObject = button.DataContext;
			object value = DataGridUtils.GetValueByPath(bindingPath,rowObject);

			if (ShowBinaryViewerFired.Fire(rowObject, bindingPath, value))
				return;

			var viewer = new BinaryViewer(){DataSource = value as byte[]};

			window.Content = viewer;

			window.ShowDialog();
		}

		public string BindingPath
		{
			get { return bindingPath; }
			set
			{
				this.bindingPath = value;
//				CellTemplate.VisualTree.FirstChild.FirstChild.SetBinding(TextBlock.TextProperty,new Binding(bindingPath));
			}
		}

		protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		{
//			FrameworkElement element = base.GenerateElement(cell, dataItem);
		
//			<StackPanel Orientation="Horizontal" Margin="2">
//				<TextBlock Text="{Binding [0]}" Margin="0,0,2,0" x:Name="textBlock"/>
//				<Button Click="ShowBinaryViewer">
//					<Image Source="../Images/binary.png"></Image>
//				</Button>
//			</StackPanel>

			return BuildElement();
		}

		public FrameworkElement BuildElement()
		{
			var panel = new StackPanel();
			panel.Orientation = Orientation.Horizontal;
			panel.Margin = new Thickness(2);

			var textBlock = new TextBlock();
			textBlock.SetBinding(TextBlock.TextProperty, new Binding(bindingPath));
			textBlock.Margin = new Thickness(0,0,2,0);

			var button = new Button();
			var source = new BitmapImage();
			
			var uri1 = new Uri("pack://application:,,,/Commons.UI.WPF;component/Images/binary.png", UriKind.Absolute);
			source.BeginInit();
			source.UriSource = uri1;
			source.EndInit();

			var image = new Image()
			              	{
			              		Source = source,
			              		Width = 16
			              	};
			
			button.Content = image;
			button.Click += ShowBinaryViewer;

			panel.Children.Add(textBlock);
			panel.Children.Add(button);
			return panel;
		}
	}
}
