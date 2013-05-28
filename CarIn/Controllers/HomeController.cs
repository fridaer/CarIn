using System;
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
            var test = new HandlerForWebServiceCalls();
            test.BeginTimers();
            // Checking Logged In Session
            try
            {
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
