using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarIn.DAL.Repositories;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;

namespace CarIn.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<User> _userRepo = new Repository<User>();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.AllUsers = _userRepo.FindAll().ToList(); 
            return View();
        }

        public ActionResult ChangePassword()
        {

            ViewBag.Message = "Ändra lösenord";

            var tmpLoggedinUser = _userRepo.FindAll().FirstOrDefault();
            var viewModelChangePassword = new ChangePasswordVm
                                                  {
                                                      userId = tmpLoggedinUser.ID,
                                                      Username = tmpLoggedinUser.Username
                                                  };

            return View(viewModelChangePassword);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordVm model)
        {
            if(ModelState.IsValid)
            {
                if(!CheckIfPasswordMatch(model.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Felaktigt lösenord");
                    return View(model);
                }
                var user = _userRepo.FindAll(x => x.Username == model.Username).FirstOrDefault();
                user.Password = HashPassword(model.NewPassword);
                _userRepo.Update(user);
            }
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        private static bool CheckIfPasswordMatch(string password)
        {
            return true;
        }
        private static string HashPassword(string password)
        {
            return password;
        }
    }
}
