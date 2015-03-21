﻿using System;
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
                var validateEmailAccount = unitOfWork.AccountRepository.GetWithFilter(x=>x.Email==model.Email);
                if (validateEmailAccount != null)
                {
                    TempData["Error"] = "El usuarion con el correo electronico: "+model.Email+" ya existe";
                    return View(model);
                }
                if (model.Password == model.ConfirmPassword)
                {
                    Mapper.CreateMap<AccountRegisterModel, Account>();
                    var account = Mapper.Map<AccountRegisterModel, Account>(model);
                    unitOfWork.AccountRepository.Insert(account);
                    unitOfWork.Save();

                    var host = HttpContext.Request.Url.Host;
                    if (host == "localhost")
                        host = Request.Url.GetLeftPart(UriPartial.Authority);
                    mail.SendWelcomeMessage(account.Name, account.Email,
                        host + "/Account/ConfirmRegistration/" + account.Id.ToString());

                    TempData["Success"] = "An email has been sent, You need to confirm your account!";
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
                var account = unitOfWork.AccountRepository.GetWithFilter(x => x.Email == model.Email && x.Password == model.Password);
                
                if (account != null)
                {
                    if (account.Activated==false)
                    {
                        TempData["Error"] = "Account is not confirmed yet, please confirm your account";
                        return View(new AccountLoginModel());
                    }
                    FormsAuthentication.SetAuthCookie(account.Id.ToString(), false);
                    return RedirectToAction("Index", "Question");
                }
                TempData["Error"] = "Email and/or password invalid";
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
                    mail.SendRecoveryEmail(account.Name,account.Email,host+"/Account/ChangePassword/?id="+account.Id);

                    TempData["Success"] = "An email has been sent with instructions to recover your password.";
                    return View(model);
                }
                ModelState.AddModelError("AccountError", "The user with the email:"+model.Email+" does not exist");
            }
            return View(model);
        }
        
        public ActionResult ChangePassword(Guid id)
        {
            var model = new ChangePasswordModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model,Guid id)
        {
            var account = unitOfWork.AccountRepository.GetById(id);
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    account.Password = model.Password;
                    unitOfWork.AccountRepository.Update(account);
                    unitOfWork.Save();
                    TempData["Success"] = "Your password has been Updated";
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

        public ActionResult ConfirmRegistration(Guid id)
        {
            var account = unitOfWork.AccountRepository.GetById(id);
            account.Activated = true;
            unitOfWork.AccountRepository.Update(account);
            unitOfWork.Save();
            TempData["Success"] = "Registration has been successful";
            return RedirectToAction("Login");
        }
    
	}
}