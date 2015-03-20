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
    {//changes
        MailGun mail = new MailGun();
        
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
        [HttpPost]
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
                    mail.SendWelcomeMessage(model.Email);
                    /* Add register successful message*/
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("Error", "Password and Confirm Passsword must be the same");
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
            if (ModelState.IsValid)
            {
                var context = new OverflowVictorContext();
                var account = unitOfWork.AccountRepository.GetWithFilter(x => x.Email == model.Email && x.Password == model.Password);
                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(account.Id.ToString(), false);
                    return RedirectToAction("Index", "Question");
                }
                ViewBag.Message = "Invalid email or password ";
            }
            
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
            if (ModelState.IsValid)
            {
                var account = unitOfWork.AccountRepository.GetWithFilter(x => x.Email == model.Email);
                if (account != null)
                {
                    var host = HttpContext.Request.Url.Host;
                    if(host=="localhost")
                        host = Request.Url.GetLeftPart(UriPartial.Authority);
                    mail.SendRecoveryEmail(account.Email,host+"/Account/ChangePassword/"+account.Id.ToString());

                    TempData["Success"] = "An email has been sent with instructions to recover your password.";
                    return View(model);
                }
                ModelState.AddModelError("AccountError", "The user with the email:"+model.Email+" does not exist");
            }
            return View(model);
        }
        
        public ActionResult ChangePassword(Guid id)
        {
            var model = new ChangePasswordModel() {OwnerId = id};
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var account = unitOfWork.AccountRepository.GetById(model.OwnerId);
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    account.Password = model.Password;
                    unitOfWork.AccountRepository.Update(account);
                    unitOfWork.Save();
                    /*Add change password  successful message*/
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("Error", "Password and Confirm Passsword must be the same");
            }
            return View(model);

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