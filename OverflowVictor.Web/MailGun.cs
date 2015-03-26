using System;
using RestSharp;

namespace OverflowVictor.Web
{
    class MailGun
    {
        public RestResponse SendWelcomeMessage(string name,string email,string url)
        {
            var client = ConfigureClient();
            var request = ConfigureMail();
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello "+name);
            request.AddParameter("text", "Congratulations , you register is almost completed" +
                                         "To complete registration go to link below!");
            request.AddParameter("text", url);
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

        public RestResponse SendRecoveryEmail(string name,string email, string url)
        {
            var client = ConfigureClient();
            var request = ConfigureMail();
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello "+name);
            request.AddParameter("text", "Somebody has recently asked to Reset your\n" +
                                         "OverflowVictorAccount, click on the link below\n" +
                                         "to reset your password");
            request.AddParameter("text", url);
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }

        public RestResponse SendLoginWarningMessage(string name, string email)
        {
            var client = ConfigureClient();
            var request = ConfigureMail();
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello " + name);
            request.AddParameter("text", "Alert, someone is trying to login with your email" +
                                         "\nwith incorrect information");
           
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }
    }
}