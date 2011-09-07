using System;
using System.Collections;
using System.Collections.Generic;
using Db4oExplorer.Domain;

namespace Db4oExplorer.Test
{
	public class Db4oLocalConnectionStub:LocalConnection
	{
		public override IList<IStoredClass> Objects
		{
			get
			{
				return new List<IStoredClass>()
				       	{
				       		new Db4oStoredClass {Name="Car"},
				       		new Db4oStoredClass {Name="Plane"},
				       	};
			}
		}

		public override bool IsConnected
		{
			get { return true; }
		}

		public override IList<NameValue> Statistics
		{
			get { throw new NotImplementedException(); }
		}

		public override void Disconnect()
		{
			
		}

		public override void TryConnect()
		{
			
		}

		public override void Defragment()
		{
			throw new NotImplementedException();
		}

		public override void CreateNewClass()
		{
			throw new NotImplementedException();
		}

		public override IList GetData(IStoredClass storedClass)
		{
			throw new NotImplementedException();
		}

		public override IList GetData(string className)
		{
			throw new NotImplementedException();
		}
	}
}
