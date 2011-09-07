using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Commons.UI.WPF.TestUtils;
using Db4oExplorer.Domain;
using Db4oExplorer.StoredClass;
using NUnit.Framework;
using Commons.Data.DemoData;

namespace Db4oExplorer.Test.StoredClass
{
	[TestFixture]
	public class StoredClassDataViewTest:ControlTest<StoredClassDataView>
	{
		protected override StoredClassDataView CreateControl()
		{
			StoredClassDataView view = new StoredClassDataView();

			view.StoredClass = new Db4oStoredClass(){Fields = new List<Field>
			                                               	{
			                                               		new Db4oField(){Name="Length"},
			                                               		new Db4oField(){Name="Type"}
			                                               	}};

			view.DataSource = new CrossbowBuilder().BuildAll(x=>new Db4oObject{Fields = new object[]
			                                                                            	{
			                                                                            		x.Length, 
																								new DbObject{Fields = new object[]{x.Type}}
			                                                                            	}});
			return view;
		}

		[Test]
		public void Show()
		{
			ShowWin();
		}

		[Test]
		[Explicit]
		public void ExplicitShow()
		{
			Show();
			RunApp();
		}
	}
}
