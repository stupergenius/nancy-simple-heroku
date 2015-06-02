using System;
using System.Text;
using Nancy;
using Nancy.ModelBinding;
using Twilio;
using Twilio.TwiML;

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
				new TwilioRestClient("asdf", "asdf");
				var response = new TwilioResponse();

				if (sms.Body.Trim() == "123") {
					response.Message("Download the app at https://play.google.com/store/apps/details?id=com.kidneysmart.kidneysmartrecommendation");
				} else if (sms.Body.Trim() == "456") {
					response.Message("Download the app at https://itunes.apple.com/us/app/kidney-smart-recommendation/id900531139?mt=8");
				} else {
					response.Message("No comprendé...");
				}

				var twiml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + response.ToString();
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
