using System;
using System.Text.RegularExpressions;

namespace Db4oExplorer.Domain
{
	public abstract class Field
	{
		public abstract void Rename(string text);
		public abstract void CreateIndex();
		public abstract string Name { get; set; }

		//TODO my own type instead of ReflectClass?
		public string DataType { get; set; }
	}

	public class NeoDatisField : Field
	{
		public override void Rename(string text)
		{
			throw new NotImplementedException();
		}

		public override void CreateIndex()
		{
			throw new NotImplementedException();
		}

		public override string Name
		{
			get; set;
		}
	}
}
