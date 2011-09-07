using System.Collections.Specialized;
using Common.Logging;
using Common.Logging.Simple;

namespace Commons.TestUtils
{
    public static class Logging
    {
        /// <summary>
        /// setup logging in tests - use this method prior to context load
        /// </summary>
        public static void Setup()
        {
            NameValueCollection properties = new NameValueCollection();
            properties["level"] = "Debug";

            LogManager.Adapter =
                new ConsoleOutLoggerFactoryAdapter(properties);

            LogManager.GetLogger(typeof(object)).Debug("Common.Logging configured");
        }
    }
}
