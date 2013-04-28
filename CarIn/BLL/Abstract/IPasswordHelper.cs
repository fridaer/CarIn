using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarIn.BLL.Abstract
{
    public interface IPasswordHelper
    {
        bool CheckIfPasswordMatch(string password, string HashedOldPassword);

        string HashPassword(string password);
    }
}