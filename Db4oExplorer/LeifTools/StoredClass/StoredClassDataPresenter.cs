using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Db4oExplorer.Domain;
using Microsoft.Windows.Controls;
using Commons.UI.WPF;
using Commons.UI.WPF.DataGrid;
using Commons.UI.WPF.Services;
using Commons.UI.WPF.Common;

namespace Db4oExplorer.StoredClass
{
	public class StoredClassDataPresenter : IStoredClassDataPresenter
	{
		private readonly IWindowManager windowManager;
		private readonly IBrowseFileService browseFileService;
		private readonly IDbObjectFactory dbObjectFactory;

		public StoredClassDataPresenter(IWindowManager windowManager, IBrowseFileService browseFileService, IDbObjectFactory dbObjectFactory)
		{
			this.windowManager = windowManager;
			this.browseFileService = browseFileService;
			this.dbObjectFactory = dbObjectFactory;
		}

		public void ShowData(IStoredClass storedClass)
		{
			StoredClassDataView view = CreateView();
			view.StoredClass = storedClass;
			
			ReloadData(view);

			view.Name = storedClass.PureName + "StoredClassDataView";

			windowManager.AddMainPane(view, storedClass.Name, "Images/class.png");
		}

		private StoredClassDataView CreateView()
		{
			var view = new StoredClassDataView();
			view.ReloadFired += new Action<StoredClassDataView>(view_ReloadFired);
			view.ShowSelectedObject += new Action<object>(view_ShowSelectedObject);
			view.ShowSelectedTypeObjects += new Action<StoredClassDataView, object>(view_ShowSelectedTypeObjects);
			view.DeleteFired += new Action<StoredClassDataView>(view_DeleteFired);
			view.EditFired += new Action<StoredClassDataView, DbObject>(view_EditFired);
			view.SaveFired += new Action<StoredClassDataView>(view_SaveFired);
			view.CancelFired += new Action<StoredClassDataView>(view_CancelFired);
			view.AddFired += new Action<StoredClassDataView>(view_AddFired);
			view.EditDbObjectFired += new Action<StoredClassDataView, DbObject, string, DbObject>(view_EditDbObjectFired);
			view.SetToNullFired += new Action<StoredClassDataView, DbObject, string>(view_SetToNullFired);
			view.ShowBinaryViewerFired += new Func<object, string, object, StoredClassDataView, bool>(view_ShowBinaryViewerFired);
			
			return view;
		}

		bool view_ShowBinaryViewerFired(object arg1, string arg2, object arg3, StoredClassDataView view)
		{
			byte[] data = arg3 as byte[];

			var viewer = new BinaryViewer();
			viewer.DataSource = data;
			viewer.BrowseFired += new Action<BinaryViewer,DbObject,string, StoredClassDataView>(viewer_BrowseFired).Apply(viewer,(DbObject)arg1,arg2, view);
			

			windowManager.ShowDialog(viewer, "Binary viewer");

			return true;
		}

		void viewer_BrowseFired(BinaryViewer binaryViewer, DbObject rowDbObject, string bindingPath, StoredClassDataView view)
		{
			BrowseResult browse = browseFileService.Browse(null);
			if (browse.Cancel) return;

			byte[] bytes = File.ReadAllBytes(browse.FilePath);

			binaryViewer.DataSource = bytes;
			binaryViewer.Path = browse.FilePath;

			DataGridUtils.SetValueByPath(rowDbObject,bindingPath,bytes);
			AddEditedObject(view,rowDbObject);
		}

		void view_SetToNullFired(StoredClassDataView view, DbObject arg2, string arg3)
		{
			DataGridUtils.SetValueByPath(arg2,arg3,null);
			AddEditedObject(view,arg2);
			view.RefreshData();
		}

		void view_EditDbObjectFired(StoredClassDataView view, DbObject rowDbObject, string bindingPath, DbObject fieldDbObject)
		{
			var dialogView = new StoredClassDataView();

			IConnection connection = view.StoredClass.Connection;
			
			if(fieldDbObject!=null)
				dialogView.StoredClass = fieldDbObject.Clazz;
			else
			{
				IList<IStoredClass> classes = connection.Objects;
				var box = new ComboBox();
				box.Height = 18;
				box.Width = 300;
				box.Name = "EditDbObject";

				box.ItemsSource = classes;

				if(!windowManager.ShowDialog(box, "What type of objects you want here?"))
					return;

				dialogView.StoredClass = box.SelectedValue as IStoredClass;
			}

			dialogView.DataSource = connection.GetData(dialogView.StoredClass);

			if (!windowManager.ShowDialog(dialogView, "Edit"))
				return;

			DbObject newFieldDbObject = dialogView.GetSelectedObject();
			DataGridUtils.SetValueByPath(rowDbObject,bindingPath,newFieldDbObject);

			AddEditedObject(view,rowDbObject);
			view.RefreshData();
		}

		void view_AddFired(StoredClassDataView view)
		{
			var dbObject = dbObjectFactory.Create(view.StoredClass);
			AddEditedObject(view,dbObject);

			var source = new ArrayList(view.DataSource);
			source.Add(dbObject);
			view.DataSource = source;
		}

		void view_CancelFired(StoredClassDataView view)
		{
			editedObjects.Remove(view);
			
			view.IsSaveCancelEnabled = false;
			ReloadData(view);
		}

		void view_SaveFired(StoredClassDataView view)
		{
			IList<DbObject> dbObjects = editedObjects[view];
			IStoredClass @class = view.StoredClass;
			@class.Save(dbObjects);

			view.IsSaveCancelEnabled = false;
			ReloadData(view);
		}

		private IDictionary<StoredClassDataView,IList<DbObject>> editedObjects = new Dictionary<StoredClassDataView, IList<DbObject>>();

		void view_EditFired(StoredClassDataView view, DbObject dbObject)
		{
			AddEditedObject(view, dbObject);
		}

		private void AddEditedObject(StoredClassDataView view, DbObject dbObject)
		{
			if(!editedObjects.ContainsKey(view))
				editedObjects.Add(view,new List<DbObject>());

			IList<DbObject> dbObjects = editedObjects[view];

			if(!dbObjects.Contains(dbObject))
				dbObjects.Add(dbObject);

			view.IsSaveCancelEnabled = true;
		}

		void view_DeleteFired(StoredClassDataView view)
		{
			if (!windowManager.Ask("Are you sure want to delete?", "Delete"))
				return;

			IList objects = view.GetSelectedObjects();
			IStoredClass @class = view.StoredClass;
			@class.Delete(objects);
			ReloadData(view);
		}

		void view_ShowSelectedObject(object obj)
		{
			DbObject dbObject = obj as DbObject;

			if (dbObject == null)
			{
				windowManager.Error(
					"You can show detailed info only on objects of your own classes. Selected object is systems or primitive.");
				return;
			}

			StoredClassDataView view = CreateView();
			view.StoredClass = dbObject.Clazz;
			view.DataSource = new ArrayList {dbObject};
			windowManager.AddMainPane(view,dbObject.ToString(),"Images/class.png");
		}

		void view_ShowSelectedTypeObjects(StoredClassDataView view, object obj)
		{
			DbObject dbObject = obj as DbObject;

			if (dbObject == null)
			{
				windowManager.Error(
					"You can show detailed info only on objects of your own classes. Selected object is systems or primitive.");
				return;
			}

			StoredClassDataView newView = CreateView();
			newView.StoredClass = dbObject.Clazz;
			newView.DataSource = view.StoredClass.Connection.GetData(newView.StoredClass);
			windowManager.AddMainPane(newView, newView.StoredClass.ToString(), "Images/class.png");
		}

		void view_ReloadFired(StoredClassDataView view)
		{
			//TODO smth like HourGlass or LightBox
			ReloadData(view);
		}

		private void ReloadData(StoredClassDataView view)
		{
//			var collection = new ObservableCollection<DbObject>();
			IList data = view.StoredClass.GetData();

//			foreach (var o in data)
//			{
//				collection.Add((DbObject) o);
//			}
//			
//			view.DataSource = collection;
			view.DataSource = data;
		}
	}
}
