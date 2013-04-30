using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CarIn.BLL.Abstract;
using System.Security.Cryptography;
using System.Globalization;

namespace CarIn.BLL
{
    public class PasswordHelper : IPasswordHelper
    {
        public bool CheckIfPasswordMatch(string clearTextPassword, string hashedPassword) 
        {
            if(!string.IsNullOrEmpty(clearTextPassword) && !string.IsNullOrEmpty(hashedPassword))
            {
                if (BCrypt.CheckPassword(clearTextPassword, hashedPassword)) 
                {
                    return true;
                }
            }
            return false;
        }

        public string HashPassword(string clearTextPassword, string saltValue)
        {
            if(!string.IsNullOrEmpty(clearTextPassword) && !string.IsNullOrEmpty(saltValue))
            {
                return BCrypt.HashPassword(clearTextPassword, saltValue);
            }
            return "";
        }


        public string GenerateSalt() 
        {
            return BCrypt.GenerateSalt(12);
        }

   
    }
}