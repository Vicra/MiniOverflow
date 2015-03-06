using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using OverflowVictor.Data;
using OverflowVictor.Domain.Entities;
using OverflowVictor.Web.Models;
using Restsharp;


namespace OverflowVictor.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController() { }
        readonly IMappingEngine _mappingEngine;

        public AccountController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }
        public ActionResult Register()
        {
            return View(new AccountRegisterModel());
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    Mapper.CreateMap<AccountRegisterModel, Account>();
                    var account = Mapper.Map<AccountRegisterModel, Account>(model);

                    var context = new OverflowVictorContext();
                    context.Accounts.Add(account);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }


        public ActionResult Login()
        {
            return View(new AccountLoginModel());
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Login(AccountLoginModel model)
        {
            var context = new OverflowVictorContext();
            var account = context.Accounts.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (account != null)
            {
                FormsAuthentication.SetAuthCookie(account.Id.ToString(), false);
                return RedirectToAction("Index", "Question");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Question");
        }


        public ActionResult RecoverPassword()
        {
            return View(new AccountRecoverPasswordModel());
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult RecoverPassword(AccountRecoverPasswordModel model)
        {
            MailGun mail = new MailGun();
            var context = new OverflowVictorContext();
            var email = context.Accounts.FirstOrDefault(x=>x.Email == model.Email);
            string message = "This is your password";
            mail.SendEmail(model.Email,"Recover Password",message);
            return RedirectToAction("Login");
        }
        public ActionResult GoToProfile(Guid ownerId)
        {
            Mapper.CreateMap<Account, AccountProfileModel>();
            var context = new OverflowVictorContext();
            var owner = context.Accounts.Find(ownerId);
            var model = Mapper.Map<Account, AccountProfileModel>(owner);
            return View(model);
        }

        
	}
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
    }
}