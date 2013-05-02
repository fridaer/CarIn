using CarIn.DAL.DbInitializers;
using CarIn.DAL.Repositories;
using CarIn.Models.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CarIn.DAL;
using CarIn.DAL.Context;
using CarIn.BLL;

namespace CarIn.Tests
{
    [TestFixture]
    class Seedtests
    {
        private User User1;
        [SetUp]
        public void Setup()
        {
            //var passHelper = new PasswordHelper();
            //var Users = new List<User>
            //{
            //    new User { Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Frida" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Fredrik" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Urban" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Nisselina" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Kalle" ,  Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Sean" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            //    new User { Username = "Albert" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Sofia" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Victoria" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Karin" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Birger" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Nisse" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Alice" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //    new User { Username = "Algott" , Password = passHelper.HashPassword("delösersiginte", passHelper.GenerateSalt())},
            //};

            ////Copy of seedfunction
        } 

        [Test]
        public void Check_if_Username_exists()
        {
            //Arrange
            var passHelper = new PasswordHelper();
            var Users = new List<User>
            {
                new User { Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            };
            
            //Act
            //Assert
            Assert.IsNotNullOrEmpty(Users.FirstOrDefault().Username);
            Console.Error.WriteLine(" Username " + Users.FirstOrDefault().Username) ;
        }
                
        [Test]
        public void Check_if_passHelper_HashPassword_Returns_Data()
        {
            //Arrange
            var passHelper = new PasswordHelper();
            var Users = new List<User>
            {
                new User { Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
            };

            //Act
            //Assert
            Assert.IsNotNullOrEmpty(Users.FirstOrDefault().Password);
            Console.Error.WriteLine(" Hashed_password " + Users.FirstOrDefault().Password) ;
        }

        [Test]
        public void Check_if_Users_List_Exist()
        {
            //Arrange
            var passHelper = new PasswordHelper();
            var Users = new List<User>
            {
                new User { Username = "Tobiasen" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},
                new User { Username = "Björn" , Password = passHelper.HashPassword("delösersig", passHelper.GenerateSalt())},

            };

            //Act
            //Assert
            Assert.IsNotNull(Users);
        }
    }
}
