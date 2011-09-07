using NUnit.Framework;


namespace Commons.Utils.Test
{
    [TestFixture]
    public class FileUtilsTest
    {
        [Test]
        public void MakeValidFilePath()
        {
            var invalid = "~\"M<>\"ary had>> a?\"<>| litt|le|| la*\"mb.?";
            var valid = FileUtils.MakeValidFilePath(invalid);
            Assert.That(valid, Is.EqualTo("~_M___ary had__ a_____ litt_le__ la__mb._"));
        }
    }
}
