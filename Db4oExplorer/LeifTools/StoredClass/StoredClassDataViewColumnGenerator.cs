using System;
using System.Windows.Data;
using Db4oExplorer.Domain;
using Microsoft.Windows.Controls;
using Commons.UI.WPF.Converters;
using Commons.UI.WPF.DataGrid;

namespace Db4oExplorer.StoredClass
{
	public class StoredClassDataViewColumnGenerator
	{
		//TODO refactor this class, it's bad style
		public DataGridColumn Generate(Field field, string bindingPath, 
			Action<DbObject, string, DbObject> OnEditDbObjectField, Func<object, string, object, bool> ShowBinaryViewFired)
		{
			DataGridColumn column;

			if(field.DataType == null)
				column = new DataGridTextColumn();
			else if (field.DataType.ToLower().Contains("bool"))
				column = new DataGridCheckBoxColumn();
			else if (field.DataType.ToLower().Contains("system.object"))
				column = new DataGridDbObjectColumn(bindingPath, OnEditDbObjectField);
			else if (field.DataType.ToLower().Contains("system.datetime"))
				column = new DataGridDateTimeColumn();
			else if (field.DataType.ToLower().Contains("system.byte"))
			{
				var binaryColumn = new DataGridBinaryColumn();
				binaryColumn.ShowBinaryViewerFired += ShowBinaryViewFired;
				column = binaryColumn;
			}
			else
				column = new DataGridTextColumn();



			column.Header = field.Name;

			DataGridBoundColumn boundColumn = column as DataGridBoundColumn;

			if (boundColumn != null)
			{
				var binding = new Binding(bindingPath) { Mode = BindingMode.TwoWay };
				boundColumn.Binding = binding;
				if (field.DataType!=null && field.DataType.ToLower().Contains("system.int32"))
					binding.Converter = new Int32Converter();

			}

			IBindableDataGridColumn bindableDataGridColumn = column as IBindableDataGridColumn;
			if (bindableDataGridColumn != null)
			{
				try
				{
					bindableDataGridColumn.BindingPath = bindingPath;
				}
				//TODO needed for DataGridDbObjectColumn - remove
				catch (NotImplementedException e)
				{
					
				}
			}


			return column;
		}
	}
}
