using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;
using Microsoft.Windows.Controls;

namespace Commons.UI.WPF.DataGrid
{
	public static class DataGridUtils
	{

		public static object GetFieldByColumn(DataGridColumn column, object value)
		{
			string path = GetPath(column);

			return GetValueByPath(path, value);
		}

		public static string GetPath(DataGridColumn column)
		{
			DataGridBoundColumn gridBoundColumn = column as DataGridBoundColumn;

			string path = null;
			if(gridBoundColumn!=null)
			{
				Binding binding = (Binding)gridBoundColumn.Binding;
				path = binding.Path.Path;
			}
			else
			{
				IBindableDataGridColumn dbObjectColumn = (IBindableDataGridColumn)column;
				path = dbObjectColumn.BindingPath;
			}
			return path;
		}

		public static object GetValueByPath(string path, object value)
		{
			object cellValue;
			
			//for indexers
			if (path.StartsWith("[") && path.EndsWith("]"))
			{
				int index = Convert.ToInt32(path.TrimStart('[').TrimEnd(']'));
				object[] attributes = value.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true);
				DefaultMemberAttribute attribute = (DefaultMemberAttribute)attributes[0];

				string memberName = attribute.MemberName;

				PropertyInfo propertyInfo = value.GetType().GetProperty(memberName);

				cellValue = propertyInfo.GetValue(value, new object[] { index });

			}
			else
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
				PropertyDescriptor propertyDescriptor = properties[path];
				cellValue = propertyDescriptor.GetValue(value);
			}

			return cellValue;
		}

		public static void SetValueByPath(object rowDbObject, string path, object newFieldDbObject)
		{
			if (path.StartsWith("[") && path.EndsWith("]"))
			{
				int index = Convert.ToInt32(path.TrimStart('[').TrimEnd(']'));
				object[] attributes = rowDbObject.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true);
				DefaultMemberAttribute attribute = (DefaultMemberAttribute)attributes[0];

				string memberName = attribute.MemberName;

				PropertyInfo propertyInfo = rowDbObject.GetType().GetProperty(memberName);

				propertyInfo.SetValue(rowDbObject, newFieldDbObject, new object[] { index });

			}
			else
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(rowDbObject);
				PropertyDescriptor propertyDescriptor = properties[path];
				propertyDescriptor.SetValue(rowDbObject,newFieldDbObject);
			}
		}
	}
}
