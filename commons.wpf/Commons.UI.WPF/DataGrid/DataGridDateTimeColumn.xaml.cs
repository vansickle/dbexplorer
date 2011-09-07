using System;
using System.Collections.Generic;
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
using Microsoft.Windows.Controls;
using Commons.UI.WPF.Converters;

namespace Commons.UI.WPF.DataGrid
{
	/// <summary>
	/// Interaction logic for DataGridDateTimeColumn.xaml
	/// </summary>
	public partial class DataGridDateTimeColumn : DataGridTemplateColumn,IBindableDataGridColumn
	{
		private string bindingPath;

		public DataGridDateTimeColumn()
		{
			InitializeComponent();
			
		}

		protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
		{
			var binding = new Binding(bindingPath);

			var textBlock = new TextBlock();
			textBlock.SetBinding(TextBlock.TextProperty, binding);
			textBlock.Margin = new Thickness(0, 0, 2, 0);

			return BuildElement(textBlock);
		}

		private FrameworkElement BuildElement(UIElement textControl)
		{
			var panel = new StackPanel();
			panel.Orientation = Orientation.Horizontal;
			panel.Margin = new Thickness(2);


			var source = new BitmapImage();

			var uri1 = new Uri("pack://application:,,,/Commons.UI.WPF;component/Images/calendar.png", UriKind.Absolute);
			source.BeginInit();
			source.UriSource = uri1;
			source.EndInit();

			var image = new Image()
			            	{
			            		Source = source,
			            		Width = 16
			            	};

			panel.Children.Add(textControl);
			panel.Children.Add(image);
			return panel;
		}

		protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
		{
			var binding = new Binding(bindingPath) {Mode = BindingMode.TwoWay, Converter = new DateTimeConverter()};

			var textBlock = new TextBox();
			textBlock.SetBinding(TextBox.TextProperty, binding);
			textBlock.Margin = new Thickness(0, 0, 2, 0);

			return BuildElement(textBlock);
		}

		public string BindingPath
		{
			get { return bindingPath; }
			set { bindingPath = value; }
		}
	}
}
