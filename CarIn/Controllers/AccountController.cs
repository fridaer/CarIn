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
            if (username == "hej")
            {
                if (password == "losen")
                {
                    Session["IsLoggedIn"] = true;
                    ViewBag.mittMeddelande = "inloggad!";
                }
            }
            else
            {
                ViewBag.mittMeddelande = "användarnamn eller lösenord är felaktigt";
            }


            return View();
        }

    }
}
