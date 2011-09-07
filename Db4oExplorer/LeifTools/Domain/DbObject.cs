using System;
using System.Collections;
using System.ComponentModel;
using System.Dynamic;
using Commons.UI.WPF.EventWiring;

namespace Db4oExplorer.Domain
{
	public class DbObject:DynamicObject, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged.Fire(this, propertyName);
		}


		protected object[] fields;
		protected IStoredClass clazz;

		public object[] Fields
		{
			get { return fields; }
			set { fields = value; }
		}

		public IStoredClass Clazz
		{
			get { return clazz; }
		}
	}
}
