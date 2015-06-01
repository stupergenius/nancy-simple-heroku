using System;
using System.Text;

namespace Nancy.Simple
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = args => {
				return View ["index", Request.Url];
			};
			Post["/sms"] = args => {
				var twiml = "<Response><Message>Hello World!</Message></Response>";
				return new Response
				{
					StatusCode = HttpStatusCode.OK,
					ContentType = "text/xml",
					Contents = (stream) => {
						var message = Encoding.UTF8.GetBytes(twiml);
						stream.Write(message, 0, message.Length);
					},
				};
			};
		}
	}
}
