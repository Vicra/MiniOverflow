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
        [HttpPost]
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

        [HttpPost]
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
        [HttpPost]
        public ActionResult RecoverPassword(AccountRecoverPasswordModel model)
        {
            Mail mail=new Mail();
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

        public class Mail
        {
            public void SendEmail(string email,string subject,string message)
            {
                MailMessage mail = new MailMessage("mywebsmpt@gmail.com", email);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.google.com";
                mail.Subject = subject;
                mail.Body = message;
                client.Send(mail);  
            }
        }
	}
}