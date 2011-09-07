using System;
using System.Collections.Generic;

namespace Db4oExplorer.Domain
{
	public class NameValueList : List<NameValue>
	{
		public override string ToString()
		{
			string result = String.Empty;
			foreach (var nameValue in this)
			{
				result += string.Format("{0} : {1}\n", nameValue.Name, nameValue.Value);
			}
			return result;
		}
	}
}
