using Commons.Utils.Delegates;

namespace Commons.TestUtils.Constraints
{
	public class Element<T>
	{
		public ElementPropertyConstraints<T> Property(FuncDelegate<object,T> func)
		{
			return new ElementPropertyConstraints<T>(func);
		}
	}
}
