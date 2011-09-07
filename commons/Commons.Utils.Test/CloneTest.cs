using System;
using NUnit.Framework;


namespace Commons.Utils.Test
{
    [TestFixture]
    public class CloneTest
    {
        internal class TestClassBase
        {
            private int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
        }

        internal class TestClass : TestClassBase, ICloneable
        {
            private TestClass parent;

            public TestClass Parent
            {
                get { return parent; }
                set { parent = value; }
            }

            #region ICloneable Members

            public object Clone()
            {
                return MemberwiseClone();
            }

            #endregion
        }

        [Test]
        public void MemberwiseCloneTest()
        {
            TestClass parentClass = new TestClass();
            parentClass.Id = 1204;
            parentClass.Parent = new TestClass();
            parentClass.Parent.Id = 1566;
            TestClass testClass = (TestClass) parentClass.Clone();
            Assert.That(parentClass.Id, Is.EqualTo(testClass.Id));
            Assert.That(parentClass.Parent.Id, Is.EqualTo(testClass.Parent.Id));
        }
    }
}
