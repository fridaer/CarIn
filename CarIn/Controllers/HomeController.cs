﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarIn.BLL;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;

namespace CarIn.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<User> _userRepo;

        public HomeController(IRepository<User> repo)
        {
            _userRepo = repo;
        }

        public ActionResult Index()
        {
            ViewBag.NameOfProject = "CarIn";
            // Checking Logged In Session
            try
            {
                if (Request.Cookies["userInfo"] != null)
                {
                    var cookieHelper = new CookieHelper();



                    HttpCookie aCookie = Request.Cookies["userInfo"];

                    if (cookieHelper.VerifyCookie(aCookie))
                    {
                        ViewBag.loggedInMessage = Server.HtmlEncode(Request.Cookies["userInfo"]["userName"]);
                        ViewBag.showChangePassLink = true;
                        Session["IsLoggedIn"] = true;
                    }


                }
                if ((bool)Session["IsLoggedIn"] == true)
                {

                    ViewBag.showChangePassLink = true;
                }
            }
            catch
            {
                ViewBag.showChangePassLink = false;

                ViewBag.loggedInMessage = "Inte inloggad";
            }


            ViewBag.AllUsers = _userRepo.FindAll().ToList(); 
            return View();
        }

        public ActionResult ChangePassword()
        {


            ChangePasswordVm viewModelChangePassword = null;
            string loggedinUser = Session != null ? Session["UserName"] as string : "TmpUserName";
            
            
            var tmpLoggedinUser = _userRepo.FindAll().FirstOrDefault();
            if(tmpLoggedinUser != null)
            {
                viewModelChangePassword = new ChangePasswordVm
                {
                    UserId = tmpLoggedinUser.ID,
                    Username = loggedinUser
                };
            }
            else
            {
                viewModelChangePassword = new ChangePasswordVm
                                              {
                                                  UserId = 0,
                                                  Username = "TmpUserNotInDb"
                                              };
            }
            return View("ChangePassword", viewModelChangePassword);

        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVm model)
        {
            var db = new DAL.Context.CarInContext();
            if(ModelState.IsValid)
            {
                var passHelper = new PasswordHelper();

                if (!passHelper.CheckIfPasswordMatch(model.OldPassword, _userRepo.FindAll(u => u.Username == Session["username"].ToString()).Select(u => u.Password).FirstOrDefault()))
                {
                    ModelState.AddModelError("OldPassword", "Felaktigt lösenord");
                    return View(model);
                }
                var user = _userRepo.FindAll(x => x.Username == model.Username).FirstOrDefault();
                var password = passHelper.HashPassword(model.NewPassword, passHelper.GenerateSalt().ToString());
                user.Password = password;
                _userRepo.Update(user);
                ViewBag.Message = "Lösenord ändrat";
                return RedirectToAction("Index");
            }
            return View("ChangePassword", model);

        }

        public ActionResult SignOut()
        {
            if (Request.Cookies["userInfo"] != null)
            {
                HttpCookie myCookie = new HttpCookie("userInfo");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            Session["IsLoggedIn"] = false;
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Kart_Demo()
        {
            return View();
        }
    }
}
