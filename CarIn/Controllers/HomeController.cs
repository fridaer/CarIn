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
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVm model)
        {
  
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
            return PartialView("ChangePassword", model);

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

                var password = passHelper.HashPassword(model.Password, passHelper.GenerateSalt().ToString());
                user.Password = password;
                user.Username = model.Username;
                _userRepo.Add(user);

                if (!string.IsNullOrEmpty(model.Username) && !string.IsNullOrEmpty(model.Password))
                {
                    var passwordHelper = new PasswordHelper();
                    var hashedPassword = _userRepo.FindAll(u => u.Username == model.Username).Select(u => u.Password).FirstOrDefault();
                    if (!string.IsNullOrEmpty(hashedPassword))
                    {
                        if (passwordHelper.CheckIfPasswordMatch(model.Password, hashedPassword))
                        {
                            var cookieHelper = new CookieHelper();

                            //Response.Cookies["domain"].Domain = "support.contoso.com";
                            Response.Cookies.Add(cookieHelper.CreateCookie(model.Username));
                            FormsAuthentication.SetAuthCookie(model.Username, false);
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
