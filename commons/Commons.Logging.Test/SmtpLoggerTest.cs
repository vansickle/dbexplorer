using System.Reflection;
using Common.Logging;
using NUnit.Framework;

namespace Commons.Logging.Test
{
    [TestFixture]
    public class SmtpLoggerTest
    {

        protected static readonly ILog LOG =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [Test]
        public void SentInfoTest()
        {
            LOG.Fatal("Test");
            LOG.Info("Test");
        }
    }
}
