using System;
using NUnit.Framework;
using Commons.Utils.Delegates;

namespace Commons.TestUtils.Constraints
{
	public static class Every
	{
//		public static CallbackedCollectionOrderedConstraint<object> OrderedBy(FuncDelegate<string,object> getValue, Action<T> onError)
//		{
//			return new CallbackedCollectionOrderedConstraint<object>(getValue, onError);
//		}

		public static CallbackedCollectionOrderedConstraint<T, ValueT> OrderedBy<T, ValueT>(FuncDelegate<ValueT, T> getValue, string expected,
		                                                                    FuncDelegate<string, T> onWhatElement)
			where ValueT : IComparable
		{
			return new CallbackedCollectionOrderedConstraint<T,ValueT>(getValue, expected, onWhatElement);
		}

		public static Element<T> Element<T>()
		{
			return new Element<T>();
		}
	}
}
