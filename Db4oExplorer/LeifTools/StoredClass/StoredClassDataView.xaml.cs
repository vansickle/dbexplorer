using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using Common.Logging;
using Db4oExplorer.Domain;
using Microsoft.Windows.Controls;
using Commons.UI.LayoutDataStore;
using Commons.UI.WPF.DataGrid;
using Commons.UI.WPF.EventWiring;
using Commons.UI.WPF.Common;
using Commons.UI.WPF.LayoutDataStore;

namespace Db4oExplorer.StoredClass
{
	/// <summary>
	/// Interaction logic for StoredClassDataView.xaml
	/// </summary>
	public partial class StoredClassDataView : UserControl,IContainsLayoutStores
	{
		#region logging

		protected static readonly ILog LOG =
			LogManager.GetLogger(typeof (StoredClassDataView));

		#endregion

		private IStoredClass storedClass;
		private StoredClassDataViewColumnGenerator columnGenerator = new StoredClassDataViewColumnGenerator();

		public IList<Field> Fields
		{
			set
			{
				int index = 0;
				foreach (Field field in value)
				{
					DataGridColumn column;

					string bindingPath = "[" + index + "]";

					column = columnGenerator.Generate(field, bindingPath, OnEditDbObjectField, ShowBinaryViewFired);
					
					grid.Columns.Add(column);
					index++;
				}	
			}
		}

		public event Func<object, string, object, StoredClassDataView, bool> ShowBinaryViewerFired;

		private bool ShowBinaryViewFired(object arg1, string arg2, object arg3)
		{
			return ShowBinaryViewerFired.Fire(arg1, arg2, arg3, this);
		}

		public event Action<StoredClassDataView, DbObject, string, DbObject> EditDbObjectFired;

		private void OnEditDbObjectField(DbObject rowDbObject, string bindingPath, DbObject fieldDbObject)
		{
			EditDbObjectFired.Fire(this,rowDbObject,bindingPath,fieldDbObject);
		}

		public IList DataSource
		{
			set
			{
				grid.ItemsSource = value;
			}
			get { return grid.ItemsSource as IList; }
		}

		public IStoredClass StoredClass
		{
			set
			{
				storedClass = value;
				Fields = value.Fields;
			}
			get { return storedClass; }
		}

		public bool IsSaveCancelEnabled
		{
			set
			{
				save.IsEnabled = value;
				cancel.IsEnabled = value;
			}
		}

		public event Action<StoredClassDataView> AddFired;
		public event Action<StoredClassDataView> ReloadFired;
		public event Action<StoredClassDataView> DeleteFired;
		public event Action<StoredClassDataView,DbObject> EditFired;
		public event Action<StoredClassDataView> SaveFired;
		public event Action<StoredClassDataView> CancelFired;

		public StoredClassDataView()
		{
			InitializeComponent();
			add.Click += new Wirer(()=>AddFired.Apply(this)).On;
			reload.Click += new Wirer(() => ReloadFired.Apply(this)).On;
			delete.Click += new Wirer(() => DeleteFired.Apply(this)).On;
			save.Click += new Wirer(() => SaveFired.Apply(this)).On;
			cancel.Click += new Wirer(() => CancelFired.Apply(this)).On;
			grid.CellEditEnding += new EventHandler<DataGridCellEditEndingEventArgs>(grid_CommittingEdit);
		}

		void grid_CommittingEdit(object sender, DataGridCellEditEndingEventArgs e)
		{
			DbObject item = (DbObject) e.Row.Item;
			EditFired.Fire(this,item);
		}

		public event Action<object> ShowSelectedObject;
		
		public event Action<StoredClassDataView,object> ShowSelectedTypeObjects;

		private void showSelectedObjectMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ShowSelectedObject.Fire(GetSelectedCellObject());
		}

		public object GetSelectedCellObject()
		{
			return DataGridUtils.GetFieldByColumn(GetSelectedColumn(), GetSelectedObject());
		}

		private DataGridColumn GetSelectedColumn()
		{
			IList<DataGridCellInfo> infos = grid.SelectedCells;

			if (infos != null && infos.Count == 1)
			{
				DataGridCellInfo info = infos[0];

				return info.Column;
			}

			return null;
		}

		public IList GetSelectedObjects()
		{
			IList<DataGridCellInfo> infos = grid.SelectedCells;

			return infos.Select(i => i.Item).ToList();
		}

		public DbObject GetSelectedObject()
		{
			IList<DataGridCellInfo> infos = grid.SelectedCells;

			return infos.Select(i => i.Item).FirstOrDefault() as DbObject;
		}

		private void showSelectedTypeObjectsMenuItem_Click(object sender, RoutedEventArgs e)
		{
			ShowSelectedTypeObjects.Fire(this,GetSelectedCellObject());
		}

		public event Action<StoredClassDataView, DbObject, string> SetToNullFired;

		private void setToNullMenuItem_Click(object sender, RoutedEventArgs e)
		{
			SetToNullFired.Fire(this,GetSelectedObject(),DataGridUtils.GetPath(GetSelectedColumn()));
		}

		/// <summary>
		/// refresh data for cases when INotifyPropertyChanged not helps
		/// </summary>
		public void RefreshData()
		{
			IList source = DataSource;
			DataSource = new ArrayList();
			DataSource = source;
		}

		private void grid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key==Key.Delete)
				DeleteFired.Fire(this);
			else if (e.Key == Key.Insert)
				AddFired.Fire(this);
		}

		public IList<ILayoutDataStore> GetLayoutStores()
		{
			return new LayoutStoreList(Name)
				.Add(new DataGridControlLayoutStore(grid, this));
		}
	}
}
