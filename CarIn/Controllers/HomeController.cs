using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarIn.BLL;
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
            ViewBag.NameOfProject = "CarIn";

            // Checking Logged In Session
            try
            {
                if ((bool)Session["IsLoggedIn"] == true)
                {
                    ViewBag.loggedInMessage = "Inloggad";
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
            var tmpLoggedinUser = _userRepo.FindAll().FirstOrDefault();
            if(tmpLoggedinUser != null)
            {
                viewModelChangePassword = new ChangePasswordVm
                {
                    UserId = tmpLoggedinUser.ID,
                    Username = tmpLoggedinUser.Username
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
            if(ModelState.IsValid)
            {
                var passHelper = new PasswordHelper();

                if(!passHelper.CheckIfPasswordMatch(model.OldPassword, model.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Felaktigt lösenord");
                    return View(model);
                }

                var user = _userRepo.FindAll(x => x.Username == model.Username).FirstOrDefault();
                var password = passHelper.HashPassword(model.NewPassword, passHelper.GenerateSalt().ToString());
                user.Password = password;
                //user.PasswordSalt = passWordArray[1];
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

        /*private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "user")
            {
                if (txtPassword.Text == "password")
                {
                    new Form2().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error: invalid password or usernam");
                }
            }
            else 
            { 
                MessageBox.Show("Error: invalid password or usernam"); 
            }
        }*/
    }
}
