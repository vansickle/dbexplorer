using System;
using System.Collections.Generic;
using System.Linq;
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
using Commons.UI.WPF.WM;

namespace Commons.UI.WPF.Controls
{
	/// <summary>
	/// Interaction logic for OKCancelControlContainer.xaml
	/// </summary>
	public partial class OKCancelControlContainer : Window
	{
		public OKCancelControlContainer()
		{
			InitializeComponent();
            this.KeyDown += new KeyEventHandler(OKCancelControlContainer_KeyDown);
        }

        void OKCancelControlContainer_KeyDown(object sender, KeyEventArgs e)
        {
            //если Ctrl-Enter - OK
            if (e.Key == Key.Select)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    //ставим фокус на кнопку, чтобы сохранились данные, если мы были в поле ввода
                    okButton.Focus();
                    DialogResult = true;
                }
                }
                //если Esc - Отмена
            else
                if (e.Key == Key.Escape)
                {
                    cancelButton.Focus();
                    DialogResult = false;
                }
            }
        

	    public OKCancelControlContainer(Control control, string caption)
            : this()
        {
            Name = control.Name;

			if (string.IsNullOrEmpty(Name))
			{
				var type = control.GetType();
				//если контрол собственный, а Name не задан - то подставляем имя типа
				Name = type.Namespace.StartsWith("System.")?null:type.Name;
			}

	    	if (string.IsNullOrEmpty(Name))
                throw new ArgumentException(Properties.Resources.ControlNameCanNotBeEmpty);
            Title = caption;

			double initialViewWidth = control.Width;
			double initialViewHeight = control.Height;

			Width = initialViewWidth + 22;
			Height = initialViewHeight + 77;

			if (control.MinWidth != 0)
				MinWidth = control.MinWidth + 22;
			
			if (control.MinHeight != 0)
				MinHeight = control.MinHeight + 77;

//			if (control.MaxWidth != 0)
//				MaxWidth = control.MaxWidth + 22;
//			
//			if (control.MaxHeight != 0)
//				MaxHeight = control.MaxHeight + 77;

			control.Width = Double.NaN;
			control.Height = Double.NaN;

			Control = control;

            //если контрол может сам посылать сообщение о закрытии - то подключаем соответствующий обработчик
            if (control is IClosable)
                ((IClosable)control).CloseFired += ClosableControl_CloseFired;

            control.Focus();
        }

	    private void ClosableControl_CloseFired()
	    {
            Close();
	    }

        public Control Control
		{
			get
			{
				return contentCanvas.Child as Control;	
			}
			set
			{
				contentCanvas.Child = value;
			}
		}

	    public bool OKCancelPanelVisible
	    {
            get { return okCancelCanvas.Visibility==Visibility.Visible; }
	        set {okCancelCanvas.Visibility = value ? Visibility.Visible : Visibility.Collapsed;}
	    }

	    private void okButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
