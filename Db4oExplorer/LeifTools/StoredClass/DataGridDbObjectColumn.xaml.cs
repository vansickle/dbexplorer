using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Db4oExplorer.Domain;
using Microsoft.Windows.Controls;
using Commons.UI.WPF.DataGrid;

namespace Db4oExplorer.StoredClass
{
	public class DataGridDbObjectColumn : DataGridTemplateColumn,IBindableDataGridColumn
	{
		private string bindingPath;

		public string BindingPath
		{
			get { return bindingPath; }
			set { throw new NotImplementedException(); }
		}

		private readonly Action<DbObject, string, DbObject> clickHandler;

		public DataGridDbObjectColumn(string bindingPath, Action<DbObject,string,DbObject> clickHandler)
		{
			this.bindingPath = bindingPath;
			this.clickHandler = clickHandler;

//			var stackPanelFactory = new FrameworkElementFactory(typeof (StackPanel));
//			stackPanelFactory.SetValue(StackPanel.OrientationProperty,Orientation.Horizontal);
//			stackPanelFactory.SetValue(Control.MarginProperty, new Thickness(1));
//			stackPanelFactory.SetValue(Control.WidthProperty,300.0);

			var gridFactory = new FrameworkElementFactory(typeof (Grid));
			gridFactory.SetValue(Control.MarginProperty, new Thickness(1));
			var columnDefinitionFactory1 = new FrameworkElementFactory(typeof(ColumnDefinition));
			columnDefinitionFactory1.SetValue(ColumnDefinition.WidthProperty,new GridLength(1,GridUnitType.Star));
			gridFactory.AppendChild(columnDefinitionFactory1);
			var columnDefinitionFactory2 = new FrameworkElementFactory(typeof(ColumnDefinition));
			columnDefinitionFactory2.SetValue(ColumnDefinition.WidthProperty,GridLength.Auto);
			gridFactory.AppendChild(columnDefinitionFactory2);

			var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
			textBlockFactory.SetBinding(TextBlock.TextProperty,new Binding(bindingPath));
			textBlockFactory.SetValue(Control.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
//			textBlockFactory.AddHandler(UIElement.MouseDownEvent,new MouseButtonEventHandler(OnClick));

			var buttonFactory = new FrameworkElementFactory(typeof (Button));
			buttonFactory.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnClick),true);
			buttonFactory.SetValue(Button.ContentProperty,"...");
//			buttonFactory.SetValue(Button.ContentProperty,new Viewbox);
			buttonFactory.SetValue(Button.HeightProperty,16.0);
			buttonFactory.SetValue(Button.PaddingProperty,new Thickness(1));
			buttonFactory.SetValue(Button.HorizontalAlignmentProperty,HorizontalAlignment.Right);
//			buttonFactory.SetValue(Button.MarginProperty,new Thickness(1));

//			stackPanelFactory.AppendChild(textBlockFactory);
//			stackPanelFactory.AppendChild(buttonFactory);
			
			gridFactory.AppendChild(textBlockFactory);
			gridFactory.AppendChild(buttonFactory);


			CellTemplate = new DataTemplate(){VisualTree = gridFactory};
//			CellTemplate.Seal();

//			CellEditingTemplate = CellTemplate;
		}

		private void OnClick(object sender, MouseButtonEventArgs e)
		{
			OnClick(sender,(RoutedEventArgs)e);
		}

		private void OnClick(object sender, RoutedEventArgs e)
		{
			DbObject rowDbObject = (DbObject) this.DataGridOwner.SelectedCells[0].Item;
			var dbObject = (DbObject) DataGridUtils.GetValueByPath(bindingPath,rowDbObject);
			clickHandler.Invoke(rowDbObject,bindingPath,dbObject);
		}
	}
}
