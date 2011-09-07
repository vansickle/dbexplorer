using System;
using System.Windows;
using System.Windows.Controls;

namespace Commons.UI.WPF.WM
{
	public class ControlContainer:Window
	{
		public ControlContainer(Control view, string title)
		{

			Title = title;
			ShowInTaskbar = false;
			SizeToContent = SizeToContent.WidthAndHeight;
			Content = view;
            Name = view.Name;
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException(Properties.Resources.ControlNameCanNotBeEmpty);

            if (view is IClosable)
			{
				IClosable closable = (IClosable) view;
				closable.CloseFired += new Action(closable_CloseFired);
			}
		}

		void closable_CloseFired()
		{
			Close();
		}
	}
}
