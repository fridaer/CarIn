using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult LogOn(string username, string password)
        {

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var passwordHelper = new PasswordHelper();
                var hashedPassword = _userRepo.FindAll(u => u.Username == username).Select(u => u.Password).FirstOrDefault();
                if(!string.IsNullOrEmpty(hashedPassword)){
                    if(passwordHelper.CheckIfPasswordMatch(password, hashedPassword))
                    {
                        Session["UserName"] = username;
                        Session["IsLoggedIn"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                }
                
            }
            
            Session["IsLoggedIn"] = false;

            return RedirectToAction("Index", "Home");
        }

    }
}
