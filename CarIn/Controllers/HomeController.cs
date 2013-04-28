using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarIn.DAL.Repositories;
using CarIn.Models.Entities;

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

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
