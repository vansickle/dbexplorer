using System;
using System.Collections.Generic;
using Commons.UI.WPF.TestUtils;
using Db4oExplorer.Domain;
using Db4oExplorer.StoredClass;
using NUnit.Framework;
using Commons.Data.DemoData;

namespace Db4oExplorer.Test.StoredClass
{
	[TestFixture]
	public class StoredClassDataViewColumnGenerationTest : ControlTest<StoredClassDataView>
	{
		protected override StoredClassDataView CreateControl()
		{
			var view = new StoredClassDataView();
			view.StoredClass = new Db4oStoredClass()
			                   	{
			                   		Fields = new List<Field>()
			                   		         	{
			                   		         		new Db4oField(){DataType = "System.String, System",Name = "Name"},
			                   		         		new Db4oField(){DataType = "System.Object, System",Name = "Country"},
			                   		         		new Db4oField(){DataType = "System.DateTime, System",Name = "CurrentTime"},
			                   		         	}
			                   	};
			view.DataSource = new CityBuilder().BuildAll().Convert((city) => new DbObject() {Fields = new object[] {city.Name,new DbObject(){Fields = new object[]{city.Country.Name,city.Country.InternetTLD}},DateTime.Now}});
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
