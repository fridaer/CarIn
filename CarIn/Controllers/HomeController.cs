using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CarIn.BLL;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;
using SUI.Helpers;

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
            // Checking Logged In Session
            try
            {

                // TODO TEMP 
                //var logger = new EventLog();
                //if (!System.Diagnostics.EventLog.SourceExists("CarinLogger"))
                //{
                //    System.Diagnostics.EventLog.CreateEventSource(
                //        "CarinLogger", "logger");
                //}

                //logger.Source = "CarinLogger";
                //logger.Log = "logger";
                //logger.Clear();
                //var tempHandler = new HandlerForWebServiceCalls(logger);
                //tempHandler.BeginTimers();
                //TEMP

             

                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated) //User is logged in via membership provider
                {
                    ViewBag.loggedInMessage = Server.HtmlEncode(Request.Cookies["userInfo"]["userName"]);
                    ViewBag.showChangePassLink = true;
                    return View();
                }
                if (Request.Cookies["userInfo"] != null) //User is not logged in but has a cookie
                {

                    var cookieHelper = new CookieHelper();
                    HttpCookie aCookie = Request.Cookies["userInfo"];

                    if (cookieHelper.SignInByCookie(aCookie))
                    {
                        ViewBag.loggedInMessage = Server.HtmlEncode(Request.Cookies["userInfo"]["userName"]);
                        ViewBag.showChangePassLink = true;
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.showChangePassLink = false;
                ViewBag.loggedInMessage = "Inte inloggad";
            }

            //User is not signed in and has no/not valid cookie
            ViewBag.AllUsers = _userRepo.FindAll().ToList(); 
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            try
            {
                ChangePasswordVm viewModelChangePassword = new ChangePasswordVm
                                                               {
                                                                   Username = Server.HtmlEncode(Request.Cookies["userInfo"]["userName"])

                                                               };
                ViewBag.showChangePassLink = true; //QUick fix

                return View("ChangePassword", viewModelChangePassword);
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVm model)
        {
  
            if(ModelState.IsValid)
            {
                var passHelper = new PasswordHelper();

                if (!passHelper.CheckIfPasswordMatch(model.OldPassword, _userRepo.FindAll(u => u.Username == model.Username)
                                                                                 .Select(u => u.Password)
                                                                                 .FirstOrDefault()))
                {
                    ModelState.AddModelError("OldPassword", "Felaktigt lösenord");
                    return View();
                }
                var user = _userRepo.FindAll(x => x.Username == model.Username).FirstOrDefault();
                var password = passHelper.HashPassword(model.NewPassword, passHelper.GenerateSalt());
                user.Password = password;
                _userRepo.Update(user);
                ViewData["Message"] = "Lösenord ändrat";
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Något gick fel prova igen";

            return RedirectToAction("Index");


        }

        public ActionResult RegisterNewUser() 
        {

            var viewModelRegisterNewUser = new RegisterNewUserVm();
            return View("RegisterNewUser", viewModelRegisterNewUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewUser(RegisterNewUserVm model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                var passHelper = new PasswordHelper();

                var password = passHelper.HashPassword(model.NewPassword, passHelper.GenerateSalt());
                user.Password = password;
                user.Username = model.NewUsername;
                _userRepo.Add(user);

                if (!string.IsNullOrEmpty(model.NewUsername) && !string.IsNullOrEmpty(model.NewPassword))
                {
                    var passwordHelper = new PasswordHelper();
                    var hashedPassword = _userRepo.FindAll(u => u.Username == model.NewUsername).Select(u => u.Password).FirstOrDefault();
                    if (!string.IsNullOrEmpty(hashedPassword))
                    {
                        if (passwordHelper.CheckIfPasswordMatch(model.NewPassword, hashedPassword))
                        {
                            var cookieHelper = new CookieHelper();

                            //Response.Cookies["domain"].Domain = "support.contoso.com";
                            Response.Cookies.Add(cookieHelper.CreateCookie(model.NewUsername));
                            FormsAuthentication.SetAuthCookie(model.NewUsername, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }

                }
            }
            return View("RegisterNewUser", model);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult iOS_Install() 
        { 
        
            return View();
        }
    }
}
