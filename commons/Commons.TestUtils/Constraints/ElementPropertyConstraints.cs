using Commons.Utils.Delegates;

namespace Commons.TestUtils.Constraints
{
	public class ElementPropertyConstraints<T>
	{
		private readonly FuncDelegate<object, T> func;

		public ElementPropertyConstraints(FuncDelegate<object, T> func)
		{
			this.func = func;
		}

		public ElementPropertyEqualConstraint<T> EqualTo(object expected)
		{
			return new ElementPropertyEqualConstraint<T>(expected,func);
		}
	}
}
