using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Logging;

namespace Commons.UI.WPF.Common
{
	/// <summary>
	/// Generates events while button is pressed
	/// 
	///  огда мы перемещаемс€ по какому либо списку или например крутим модель,
	/// нам надо переместитьс€ не на одно деление, а на много, мы не нажимаем кнопку много раз,
	/// а зажимаем ее. ƒанный класс реализует вызов действи€ с некоторой периодичностью при зажатой
	/// кнопке. 
	/// </summary>
	public class ButtonPresser
	{
		#region logging

		protected static readonly ILog LOG =
			LogManager.GetLogger(typeof (ButtonPresser));

		#endregion

		private Timer timer;

		private Button button;
		private readonly Action action;

		public ButtonPresser(Button button, Action action)
		{
			button.PreviewMouseDown += OnMouseDown;
			button.PreviewMouseUp += OnMouseUp;
			this.button = button;
			this.action = action;
		}

		public ButtonPresser(Button button):this(button,null)
		{
		}

		private void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			LOG.Debug("Mouse down");
			SynchroAction(null);

			//√енерируем событи€ с периодичностью в 1 сек при зажатой кнопке
			timer = new Timer(SynchroAction, null, 200, 100);
		}

		void OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			LOG.Debug("Mouse up");
			if(timer!=null)
			{
				timer.Dispose();
				timer = null;
			}
		}

		/// <summary>
		/// invoke action on main thread
		/// </summary>
		/// <param name="state"></param>
		private void SynchroAction(object state)
		{
			LOG.Debug("Invoke synchronized action");

			button.Dispatcher.Invoke(new Action(Action));
		}

		protected virtual void Action()
		{
			LOG.Debug("Invoke action");

			action.Invoke();
		}
	}
}
