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
        public UnitOfWork unitOfWork = new UnitOfWork();
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
                    unitOfWork.AccountRepository.Insert(account);
                    unitOfWork.Save();
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
            if (ModelState.IsValid)
            {
                var context = new OverflowVictorContext();
                var account = unitOfWork.AccountRepository.GetWithFilter(x => x.Email == model.Email && x.Password == model.Password);
                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(account.Id.ToString(), false);
                    MailGun mail = new MailGun();
                    mail.SendWelcomeMessage(model.Email);
                    return RedirectToAction("Index", "Question");
                }
            }
            ViewBag.Message="Invalid email or password ";
            return View(new AccountLoginModel());
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
            @ViewBag.Message = "Email sent";
            MailGun mail = new MailGun();
            var email = unitOfWork.AccountRepository.GetWithFilter(x => x.Email == model.Email);
            string message = "This is your password";
            mail.SendRecoveryEmail(model.Email,"Recover Password",message);
            return RedirectToAction("Login");
        }
        public ActionResult GoToProfile(Guid ownerId)
        {
            Mapper.CreateMap<Account, AccountProfileModel>();
            var owner = unitOfWork.AccountRepository.GetById(ownerId);
            var model = Mapper.Map<Account, AccountProfileModel>(owner);
            return View(model);
        }   
	}
}