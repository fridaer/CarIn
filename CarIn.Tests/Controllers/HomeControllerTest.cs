using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarIn.Controllers;
using CarIn.DAL.Repositories;
using CarIn.DAL.Repositories.Abstract;
using CarIn.Models.Entities;
using CarIn.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace CarIn.Tests.Controllers
{
    [TestFixture]
    class HomeControllerTest
    {
        private HomeController _homeController;
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IRepository<User>>();
            mock.Setup(x => x.Add(new User()));
            _homeController = new HomeController(mock.Object);
        }
        [Test]

        public void Index_Action_Not_Returns_Null()
        {
            //Arrange
            //Act
            var result = _homeController.Index();
            //Assert
            Assert.NotNull(result, "Should not return null");
        }
        [Test]
        public void ChangePassword_Action_Returns_ChangePassword_View()
        {
            //Arrange
            const string expected = "ChangePassword";
            //Act
            var result = _homeController.ChangePassword() as ViewResult;

            //Assert
            Assert.AreEqual(expected, result.ViewName, "Should return ChangePassword view");
        }

        [Test]
        public void ChangePassword_Action_Returns_ViewModel_ChangePasswordVm()
        {
            //Arrange
            //Act
            var result = _homeController.ChangePassword() as ViewResult;

            //Assert
            Assert.IsInstanceOf(typeof (ChangePasswordVm), result.Model, "Should return model of type ChangePasswordVm");
        }
        [Test]
        public void ChangePassword_Action_Returns_ChangePassword_View_When_Model_Is_Invalid()
        {
            //Arrange
            const string expected = "ChangePassword";
            var changePasswordVm = new ChangePasswordVm();
            _homeController.ModelState.AddModelError("Error", "ErrorMsg");
            
            //Act
            var result = _homeController.ChangePassword(changePasswordVm) as ViewResult;

            //Assert
            Assert.AreEqual(expected, result.ViewName, "Should be ChangePassword view");
        }
    }
}
