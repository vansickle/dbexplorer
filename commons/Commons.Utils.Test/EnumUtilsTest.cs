using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace Commons.Utils.Test
{
    [TestFixture]
    public class EnumUtilsTest
    {
        [Test]
        public void IsCotainedIn()
        {
            Assert.That(EnumUtils.IsContainedIn(EnumStub.ONE,EnumStub.TWO,EnumStub.THREE),Is.False);
            Assert.That(EnumUtils.IsContainedIn(EnumStub.ONE,EnumStub.TWO,EnumStub.ONE),Is.True);
        }

        enum EnumStub
        {
            ONE = 1,
            TWO = 2,
            THREE = 3
        }
        [Test, 
        Description(@"Int в Enum переводиться простым приведением, 
а не так как описано в Wiki: http://s400/Wiki/ConvertEnum2Integer.ashx")]
        public void CastTest()
        {
            EnumStub stub=EnumStub.ONE;
            int iStub = (int) stub;
            EnumStub newStub = (EnumStub) iStub;
            Assert.That(newStub, Is.EqualTo(stub));
        }
    }
}
