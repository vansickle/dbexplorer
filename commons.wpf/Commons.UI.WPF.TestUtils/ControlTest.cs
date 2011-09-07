using System;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;

namespace Commons.UI.WPF.TestUtils
{
	//TODO save control position and size on dev machine
	public abstract class ControlTest<T>:ViewTest<Window> where T:FrameworkElement
	{
		private T control;

		[SetUp]
		public virtual void SetUp()
		{
			control = CreateControl();
			window = CreateWindow(control);
		}

		private Window CreateWindow(T control)
		{
			Window win = new Window();
		    win.Name = "TestWindow";
			control.Height = Double.NaN;
			control.Width = Double.NaN;
			win.Content = Control;
			return win;
		}
	    public string WindowName
	    {
            get { return window == null ? string.Empty : window.Name; }
	    }
		public T Control
		{
			get { return control; }
		}

		protected abstract T CreateControl();
	}

}
