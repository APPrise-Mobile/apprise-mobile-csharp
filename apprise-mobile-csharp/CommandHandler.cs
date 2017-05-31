
using System;

namespace AppriseMobile
{
    public class CommandHandler
    {
		public Action<string> PrintHelp { get; }
		public Action<ApiClient, string, string[]> Handle { get; }

        public CommandHandler(Action<string> printHelp, Action<ApiClient, string, string[]> handle)
		{
			PrintHelp = printHelp;
			Handle = handle;
		}
    }
}
