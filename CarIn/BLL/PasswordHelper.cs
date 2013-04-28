using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.BLL.Abstract;

namespace CarIn.BLL
{
    public class PasswordHelper : IPasswordHelper
    {
        public bool CheckIfPasswordMatch(string PlainPassword, string HashedOldPassword)
        {
            return true;
        }

        public string HashPassword(string password)
        {
            return password;
        }

   
    }
}