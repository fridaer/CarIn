using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarIn.Controllers;
using CarIn.Models.ViewModels;
using NUnit.Framework;

namespace CarIn.Tests.Controllers
{
    [TestFixture]
    class PasswordHelperTest
    {
        private BLL.PasswordHelper passWordHelper;
        [SetUp]
        public void Setup()
        {
            passWordHelper = new BLL.PasswordHelper();
        }

        [Test]
        public void CheckIfPasswordMatch_Return_False_If_Current_PassWord_Is_Null()
        {
            //Arrange
            //Act
            var result = passWordHelper.CheckIfPasswordMatch("", "Test");
            //Assert
            Assert.IsFalse(result, "Shuld be false");
        }
        [Test]
        [Category("SlowTest")]
        public void CheckIfPasswordMatch_Return_True_If_Current_PassWord_Is_Correct()
        { 
            //Arrange
            //Act
            var salt = passWordHelper.GenerateSalt();
            var paswdHash = passWordHelper.HashPassword("test", salt);
            var result = passWordHelper.CheckIfPasswordMatch("test", paswdHash);
            //Assert
            Assert.IsTrue(result, "Should be true");
        }
        [Test]
        public void GenerateSalt_Return_Salt_Shuld_Return_A_Salt_In_String()
        {
            //Arrange
            //Act
            var result = passWordHelper.GenerateSalt();
            //Assert
            Assert.IsNotNullOrEmpty(result, "Should not be null or empty");
        }
        //[Test]
        //public void CheckIfPasswordMatch_Return_True_If_Current_PassWord_Is_Correct()
        //{
        //    //Arrange
        //    //Act
        //    var currentPassWordAndSalt = passWordHelper.HashPassword("test", "mysalt");
        //    string currentPasswordHash = currentPassWordAndSalt[0];
        //    string currentSalt = currentPassWordAndSalt[1];
        //    var result = passWordHelper.CheckIfPasswordMatch("test", currentPasswordHash, currentSalt);
        //    //Assert
        //    Assert.IsTrue(result, "Shuld be true");
        //}

    }
}
