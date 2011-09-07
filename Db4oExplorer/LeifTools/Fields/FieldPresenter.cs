using System;
using System.Windows.Controls;
using Db4oExplorer.Domain;
using Commons.UI.WPF;

namespace Db4oExplorer.Fields
{
	public class FieldPresenter : IFieldPresenter
	{
		private readonly IWindowManager windowManager;

		public FieldPresenter(IWindowManager windowManager)
		{
			this.windowManager = windowManager;
		}

		public void RenameField(Field field)
		{
			var textBox = new TextBox() { Text = field.Name, Height = 18, Width = 300};
			if (!windowManager.ShowDialog(textBox, "Rename field"))
				return;

			field.Rename(textBox.Text);
		}

		public void IndexField(Field field)
		{
			if(!windowManager.Ask("Are you sure want to create index?","Are you sure?"))
				return;

			field.CreateIndex();
		}
	}
}
