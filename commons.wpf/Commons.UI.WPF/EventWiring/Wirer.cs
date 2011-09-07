using System;
using System.Windows;

namespace Commons.UI.WPF.EventWiring
{
	/// <summary>
	/// <rus>Предназначен для "связывания" событие, т.е. когда нам на основании вызова одного события надо вызвать другое,
	/// Характерное применение - когда один Control вложен в другой и мы должен передать событие из внутреннего контрола наверх,
	/// для этого достаточно просто определить в родительском контроле такое же событие и связать его в конструкторе следующим кодом:</rus>
	/// <example>modelControl.ZoomInFired += new Wirer(() => ZoomInFired).On;</example>
	///	<rus>т.е. данный класс позволяет не создавать отдельные обработчик для события из внутреннего контрола, реализовать в нем проверки, подвязывать его</rus>
	/// <rus><see cref="StaticWirer"/>, в отличие от данного класса получает ссылку на внешнее событие, а не вызывается через delegate/lambda, т.е. у него проще синтаксис, но
	/// он должен создавать после того как обработчики подвязаны к внешним событиям, а не в конструкторе, например создать следующий метод в родительском классе и вызвать его после того
	/// как приложение (или контейнер а-ля Spring) инициализирует все связанные объекты и подвяжет их к событиям</rus>
	/// <example>
	///	public void Init()
	///		{
	///			modelControl.ZoomInFired += new StaticWirer(ZoomInFired).On;
	///			modelControl.ZoomOutFired += new StaticWirer(ZoomOutFired).On;
	///		}
	/// </example>
	/// <rus> <see cref="ActionHelper.Wire"/> - аналогично <see cref="StaticWirer"/>, но с использованием Extension Methods. </rus>
	/// <example>
	/// modelControl.ZoomInFired += ZoomInFired.Wire;
	/// </example>
	/// <rus><see cref="Wirer{T1}"/>,<see cref="Wirer{T1,T2}"/> - Wirers для событий с параметрами</rus>
	/// </summary>
	public class Wirer
	{
		private Func<Action> func;

		public Wirer(Func<Action> func)
		{
			this.func = func;
		}


		public void On()
		{
			Action action = func.Invoke();
			if (action != null)
				action.Invoke();
		}

		public void On(object sender, RoutedEventArgs e)
		{
			On();
		}
	}

	/// <summary>
	/// <see cref="Wirer"/>
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	public class Wirer<T1>
	{
		private Func<Action<T1>> func;

		public Wirer(Func<Action<T1>> func)
		{
			this.func = func;
		}


		public void On(T1 value)
		{
			Action<T1> action = func.Invoke();
			if (action != null)
				action.Invoke(value);
		}
	}

	/// <summary>
	/// <see cref="Wirer"/>
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	public class Wirer<T1, T2>
	{
		private Func<Action<T1, T2>> func;

		public Wirer(Func<Action<T1, T2>> func)
		{
			this.func = func;
		}


		public void On(T1 value1, T2 value2)
		{
			Action<T1, T2> action = func.Invoke();
			if (action != null)
				action.Invoke(value1, value2);
		}
	}
}
