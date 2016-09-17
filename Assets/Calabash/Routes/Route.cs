using System;
using System.Net;

namespace Calabash
{
	public abstract class Route
	{
		public abstract string HandleRequest(HttpListenerRequest request);
	}
}

