using System;

namespace Calabash
{
	public class FileLog
	{
		public static object lockObject = new object();

		public static void Log(string text) {
			lock (lockObject) {
				using (var stream = System.IO.File.AppendText("/tmp/calabash_unity_server.log")) {
					stream.WriteLine(text);
					stream.Flush();
				}
			}
		}
	}
}

