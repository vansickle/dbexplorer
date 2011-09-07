using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Commons.UI.WPF
{
    public abstract class AbstractMainWindow : Window
	{
        public AbstractMainWindow()
	    {
            Name = "AbstractMainWindow";
	    }

	    public abstract void AddTabPane(Control control, object caption, object icon, Action OnClose);
	}
}
