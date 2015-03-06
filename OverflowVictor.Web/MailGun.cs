using System;
using RestSharp;

namespace OverflowVictor.Web
{
    class MailGun
    {
        public RestResponse SendWelcomeMessage(string email)
        {
            var client = ConfigureClient();
            var request = ConfigureMail();
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello Victor");
            request.AddParameter("text", "Congratulations Victor, you register was succesfully completed");
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }

        public RestRequest ConfigureMail()
        {
            var request = new RestRequest();
            request.AddParameter("domain", "sandbox53ba6c1c19ac44df9f33aab21a9652ce.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox53ba6c1c19ac44df9f33aab21a9652ce.mailgun.org>");
            request.Method = Method.POST;
            return request;
        }

        public RestClient ConfigureClient()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v2");
            client.Authenticator = new HttpBasicAuthenticator("api", "key-d3d6275966a0d01704ba582b9bbd30cd");
            return client;
        }

        public RestResponse SendRecoveryEmail(string email, string recoverPassword, string message)
        {
            var client = ConfigureClient();
            var request = ConfigureMail();
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello Victor");
            request.AddParameter("text", message);
            request.AddParameter("text", "this is your password "+recoverPassword);
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }
    }
}