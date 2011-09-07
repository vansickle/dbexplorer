using System;
using System.Collections;
using NUnit.Framework.Constraints;
using Commons.Utils.Delegates;

namespace Commons.TestUtils.Constraints
{
	public class ElementPropertyEqualConstraint<T>:EqualConstraint
	{
		private readonly FuncDelegate<object, T> func;

		public ElementPropertyEqualConstraint(object expected, FuncDelegate<object, T> func)
			: base(expected)
		{
			this.func = func;
		}

		public override bool Matches(object actual)
		{
			IEnumerable enumerable = actual as IEnumerable;

			if (enumerable == null)
				throw new ArgumentException("The actual value must be IEnumerable", "actual");

			foreach (T item in enumerable)
			{
				if (!(base.Matches(func(item)))) return false;
			}
			return true;
		}
	}
}
