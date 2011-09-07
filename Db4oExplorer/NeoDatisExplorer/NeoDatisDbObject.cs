using System;
using System.Linq;
using NeoDatis.Odb.Core.Layers.Layer2.Meta;
using Db4oExplorer.Common;

namespace Db4oExplorer.Domain
{
	public class NeoDatisDbObject : DbObject
	{
		private readonly NonNativeObjectInfo nnoi;

		public NeoDatisDbObject(NonNativeObjectInfo nnoi)
		{
			this.nnoi = nnoi;
			Fields = nnoi.GetAttributeValues().Select(attr => attr.GetObject()).ToArray();
		}

		public object this[int i]
		{
			get
			{
				return fields[i];
			}
			set
			{
				fields[i] = value;
				RaisePropertyChanged("[" + i + "]");
			}
		}

		public void UpdateGenericObject()
		{
			AbstractObjectInfo[] abstractObjectInfos = nnoi.GetAttributeValues();

			ArrayHelper.ForEach(fields, (i, o) =>
			               	{
			               		NeoDatisDbObject dbObject = o as NeoDatisDbObject;

			               		if (dbObject != null)
			               			o = dbObject.GenericObject;

			               		AbstractObjectInfo abstractObjectInfo = abstractObjectInfos[i];
			               		abstractObjectInfo.SetObject(o);
								
//			               		nnoi.SetAttributeValue(i,(AbstractObjectInfo) o);
			               	});
		}

		public NonNativeObjectInfo GenericObject
		{
			get { return nnoi; }
		}
	}
}
