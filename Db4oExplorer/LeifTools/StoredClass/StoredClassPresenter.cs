using System;
using System.Windows.Controls;
using Db4oExplorer.Domain;
using Commons.UI.WPF;

namespace Db4oExplorer.StoredClass
{
	public class StoredClassPresenter : IStoredClassPresenter
	{
		private readonly IWindowManager windowManager;

		public StoredClassPresenter(IWindowManager windowManager)
		{
			this.windowManager = windowManager;
		}

		public void RenameClass(IStoredClass obj)
		{
			var textBox = new TextBox { Text = obj.Name, Width = 300, Height = 18, Name = "RenameClass"};
			if (!windowManager.ShowDialog(textBox, "Rename class"))
				return;

			obj.Rename(textBox.Text);
		}

		public void CreateNew(IConnection conn)
		{
			conn.CreateNewClass();
		}
	}
}
