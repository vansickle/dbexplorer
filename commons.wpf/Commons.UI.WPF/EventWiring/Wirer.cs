using System;
using System.Windows;

namespace Commons.UI.WPF.EventWiring
{
	/// <summary>
	/// <rus>������������ ��� "����������" �������, �.�. ����� ��� �� ��������� ������ ������ ������� ���� ������� ������,
	/// ����������� ���������� - ����� ���� Control ������ � ������ � �� ������ �������� ������� �� ����������� �������� ������,
	/// ��� ����� ���������� ������ ���������� � ������������ �������� ����� �� ������� � ������� ��� � ������������ ��������� �����:</rus>
	/// <example>modelControl.ZoomInFired += new Wirer(() => ZoomInFired).On;</example>
	///	<rus>�.�. ������ ����� ��������� �� ��������� ��������� ���������� ��� ������� �� ����������� ��������, ����������� � ��� ��������, ����������� ���</rus>
	/// <rus><see cref="StaticWirer"/>, � ������� �� ������� ������ �������� ������ �� ������� �������, � �� ���������� ����� delegate/lambda, �.�. � ���� ����� ���������, ��
	/// �� ������ ��������� ����� ���� ��� ����������� ��������� � ������� ��������, � �� � ������������, �������� ������� ��������� ����� � ������������ ������ � ������� ��� ����� ����
	/// ��� ���������� (��� ��������� �-�� Spring) �������������� ��� ��������� ������� � �������� �� � ��������</rus>
	/// <example>
	///	public void Init()
	///		{
	///			modelControl.ZoomInFired += new StaticWirer(ZoomInFired).On;
	///			modelControl.ZoomOutFired += new StaticWirer(ZoomOutFired).On;
	///		}
	/// </example>
	/// <rus> <see cref="ActionHelper.Wire"/> - ���������� <see cref="StaticWirer"/>, �� � �������������� Extension Methods. </rus>
	/// <example>
	/// modelControl.ZoomInFired += ZoomInFired.Wire;
	/// </example>
	/// <rus><see cref="Wirer{T1}"/>,<see cref="Wirer{T1,T2}"/> - Wirers ��� ������� � �����������</rus>
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
