using NUnit.Framework;

namespace Commons.Utils.Test
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void LastNSymbolsTest()
        {
            Assert.That(StringUtils.LastNsymbols("123456789", 3), Is.EqualTo("789"));
        }

        [Test]
        public void HyphenationTest()
        {
            Assert.That(StringUtils.Hyphenation("qwerty fere gylnu",6).Length, Is.EqualTo(3));
        }
		[Test]
		public void Translit()
		{
			string test = "test";
			Assert.That(test, Is.EqualTo(Transliter.Rus2Lat(test)));
			test = "“Ûst";
			Assert.That(test, Is.Not.EqualTo(Transliter.Rus2Lat(test)));
		}
    }
}
