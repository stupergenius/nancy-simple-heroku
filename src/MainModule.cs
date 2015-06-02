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
				var responseBody = "";
				
				if (sms.Body.Trim() == "123") {
					responseBody = "Download the app at https://play.google.com/store/apps/details?id=com.kidneysmart.kidneysmartrecommendation";
				} else if (sms.Body.Trim() == "456") {
					responseBody = "Download the app at https://itunes.apple.com/us/app/kidney-smart-recommendation/id900531139?mt=8";
				}

				var twiml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Response>" + responseBody + "</Response>";
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
