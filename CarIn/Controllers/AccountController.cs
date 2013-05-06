using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarIn.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string username, string password)
        {

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var db = new DAL.Context.CarInContext();
                var passwordHelper = new BLL.PasswordHelper();
                var hashedPassword = db.Users.Where(u => u.Username == username).Select(u => u.Password).FirstOrDefault();
                if(!string.IsNullOrEmpty(hashedPassword)){
                    if(passwordHelper.CheckIfPasswordMatch(password, hashedPassword))
                    {
                        Session["IsLoggedIn"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                }
                
            }
            
            Session["IsLoggedIn"] = false;

            //if (username == "hej")
            //{
            //    if (password == "losen")
            //    {
            //        Session["IsLoggedIn"] = true;
            //    }
            //}
            //else
            //{
            //}

            return RedirectToAction("Index", "Home");
        }

    }
}
