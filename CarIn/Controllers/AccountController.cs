﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarIn.BLL;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;

namespace CarIn.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepo;

        public AccountController(IRepository<User> repo)
        {
            _userRepo = repo;
        }

        [HttpPost]
        public ActionResult LogOn(string LoginUserName, string LoginPassword)
        {

            if (!string.IsNullOrEmpty(LoginUserName) && !string.IsNullOrEmpty(LoginPassword))
            {
                var passwordHelper = new PasswordHelper();
                var hashedPassword = _userRepo.FindAll(u => u.Username == LoginUserName).Select(u => u.Password).FirstOrDefault();
                if(!string.IsNullOrEmpty(hashedPassword)){
                    if (passwordHelper.CheckIfPasswordMatch(LoginPassword, hashedPassword))
                    {
                        var cookieHelper = new CookieHelper();

                        //Response.Cookies["domain"].Domain = "support.contoso.com";
                        Response.Cookies.Add(cookieHelper.CreateCookie(LoginUserName));
                        FormsAuthentication.SetAuthCookie(LoginUserName, false);
                        TempData["Message"] = "Välkommen";
                        return RedirectToAction("Index", "Home");
                    }
                }
                
            }

            return RedirectToAction("Index", "Home");
        }

        public bool SignInByCookie(HttpCookie aCookie) 
        {
            var cookieHelper = new CookieHelper();
            if (cookieHelper.VerifyCookie(aCookie)) 
            {
                string userName = Request.Cookies["userInfo"]["userName"];
                FormsAuthentication.SetAuthCookie(userName, false);
                return true;
            }
            return false;
        }

        public ActionResult SignOut()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                HttpCookie myCookie = new HttpCookie("userInfo");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated) 
            {
                FormsAuthentication.SignOut();
            }
            TempData["Message"] = "Utloggad";
            return RedirectToAction("Index","Home");
        }

        public ActionResult AccountStatus()
        {
            var VM = new Models.ViewModels.AccountStatus();
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try 
                {
                    VM.UserName = Request.Cookies["userInfo"]["userName"];
                    return PartialView("AccountStatus", VM);
                }
                catch 
                {
                    RedirectToAction("Index", "Home");
                }


            }

            var cookieHelper = new CookieHelper();
            HttpCookie aCookie = Request.Cookies["userInfo"];
            if (Request.Cookies["userInfo"] != null && cookieHelper.VerifyCookie(aCookie))
            {

                VM.UserName = Request.Cookies["userInfo"]["userName"];
                FormsAuthentication.SetAuthCookie(VM.UserName, false);
                return PartialView("AccountStatus", VM);
            }
            return PartialView("AccountStatus", VM);
        }

    }
}
