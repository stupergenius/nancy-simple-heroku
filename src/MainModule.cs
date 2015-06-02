using System;
using System.Text;
using Nancy;
using Nancy.ModelBinding;

namespace Nancy.Simple
{
	public class SMS
	{
	  public string Body { get; set; }
	}

	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = parameters => {
				return View ["index", Request.Url];
			};
			Post["/sms"] = parameters => {
				var sms = this.Bind<SMS>();
				Console.WriteLine("UserName: {0}", sms.Body);
				var twiml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Response><Message>Hello World!</Message></Response>";
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
