using System.Collections.Specialized;

namespace Commons.Logging
{
	public class ConventionalLog4NetLoggerFactoryAdapter:ExtendedLog4NetLoggerFactoryAdapter
	{
//		<arg key="configType" value="FILE" />
//		<arg key="configFile" value="~/log.config" />
//		<arg key="optionalConfigFile" value="~/log.config.local" />
		public ConventionalLog4NetLoggerFactoryAdapter() : base(new NameValueCollection()
		                                                        	{
		                                                        		{"configType","FILE"},
		                                                        		{"configFile","~/log.config"},
		                                                        		{"optionalConfigFile","~/log.config.local"},
		                                                        	})
		{
		}
	}
}
