using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace CarIn.BLL.Abstract
{
    public interface IPasswordHelper
    {
        bool CheckIfPasswordMatch(string clearTextPassword, string hashedPassword);

        string HashPassword(string clearData, string saltValue);

        string GenerateSalt();
    }
}