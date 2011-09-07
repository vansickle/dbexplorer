using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Reflect;
using Commons.Utils;

namespace Db4oExplorer.Domain
{
	public class Db4oField:Field
	{
		private readonly IStoredField storedField;
		private string internalName;


		public Db4oField()
		{
		}

		public Db4oField(IStoredField storedField)
		{
			this.storedField = storedField;
			internalName = storedField.GetName();
			name = AutomaticPropertyUtils.TryParseName(internalName);
			IsIndexed = storedField.HasIndex();
			
			IReflectClass type = storedField.GetStoredType();
			if(type!=null)
				DataType = type.ToString();
		}

		private bool isIndexed;
		public bool IsIndexed
		{
			get { return isIndexed; }
			set { isIndexed = value; }
		}

		public Db4oField(IReflectField storedField)
		{
			internalName = storedField.GetName();
			name = AutomaticPropertyUtils.TryParseName(internalName);

			IReflectClass fieldType = storedField.GetFieldType();
			if(fieldType!=null)
				DataType = fieldType.ToString();
		}

		private string name;
		public override string Name
		{
			get { return name; }
			set
			{
				if (name != internalName)
					internalName = AutomaticPropertyUtils.GetFieldName(name);
				name = value;
			}
		}

		public override string ToString()
		{
			return string.Format("{0}", Name);
		}

		public bool Equals(Db4oField other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Name, Name);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Field)) return false;
			return Equals((Field) obj);
		}

		public override int GetHashCode()
		{
			return (Name != null ? Name.GetHashCode() : 0);
		}

		public override void Rename(string newName)
		{
			if (name != internalName)
				newName = AutomaticPropertyUtils.GetFieldName(newName);

			storedField.Rename(newName);
		}

		public override void CreateIndex()
		{
			storedField.CreateIndex();
		}
	}
}
