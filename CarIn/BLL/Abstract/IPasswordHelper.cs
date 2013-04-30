using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace CarIn.BLL.Abstract
{
    public interface IPasswordHelper
    {
        bool CheckIfPasswordMatch(string clearData, string hashedPassword, string saltValue, HashAlgorithm hash);

        string[] HashPassword(string clearData, string saltValue, HashAlgorithm hash);
    }
}