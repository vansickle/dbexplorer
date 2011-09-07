using System;
using System.Collections;
using NUnit.Framework.Constraints;
using Commons.Utils.Delegates;

namespace Commons.TestUtils.Constraints
{
	public class CallbackedCollectionOrderedConstraint<T,ValueT> : Constraint where ValueT : IComparable
	{
		private readonly FuncDelegate<ValueT, T> getValue;
		private readonly string expected;
		private readonly FuncDelegate<string, T> onWhatElement;
		private string whatElement;

		public CallbackedCollectionOrderedConstraint(FuncDelegate<ValueT, T> getValue, string expected, FuncDelegate<string,T> onWhatElement)
		{
			this.getValue = getValue;
			this.expected = expected;
			this.onWhatElement = onWhatElement;
		}

		public override void WriteActualValueTo(MessageWriter writer)
		{
			writer.WriteActualValue(whatElement);
		}

		public override bool Matches(object actual)
		{
			IEnumerable enumerable = actual as IEnumerable;

			if (enumerable == null)
				throw new ArgumentException("The actual value must be IEnumerable", "actual");

			ValueT current = default(ValueT);
			foreach (T item in enumerable)
			{
				ValueT newValue = getValue(item);
				if (newValue.CompareTo(current) < 0)
				{
					whatElement = onWhatElement(item);
					return false;
				}
				current = newValue;
			}
			return true;
		}

		public override void WriteDescriptionTo(MessageWriter writer)
		{
//			writer.WriteMessageLine(whatElement);
//			writer.WritePredicate(expected);
//			writer.WriteA(whatElement);
//			writer.DisplayDifferences(expected,whatElement);
			writer.WriteExpectedValue(expected);
//			writer.WriteActualValue(whatElement);

//			writer.WritePredicate("values not ordered, starts with:");
//			writer.WriteValue(whatElement);
		}
	}
}
