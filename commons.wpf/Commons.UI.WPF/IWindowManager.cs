using System.Windows;
using System.Windows.Controls;

namespace Commons.UI.WPF
{
	public interface IWindowManager
	{
		void AddMainPane(Control control, object caption, object icon);
		
		//TODO SHOW_DIALOG optional icon
		bool ShowDialog(Control view, string title);
	    bool ShowDialog(object obj, string caption, bool showOkCancelPanel);
		void Show(Control view, string title);
		bool Ask(string text, string header);
		void Error(string errorMessage);
		void ShowWindow(Window window);
	}
}
