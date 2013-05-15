using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;


namespace CarIn.BLL
{
    public class CookieHelper
    {
        public HttpCookie CreateCookie(string username)
        {
            var passwordHelper = new PasswordHelper();
            var cookieExpDate = DateTime.Now.AddDays(1);
            HttpCookie aCookie = new HttpCookie("userInfo");
            aCookie.Values["userName"] = username;
            aCookie.Values["lastVisit"] = DateTime.Now.ToString();
            aCookie.Values["hashKey"] = passwordHelper.HashPassword(username + cookieExpDate.ToString(), passwordHelper.GenerateSalt());
            aCookie.Values["exp"] = cookieExpDate.ToString();
            aCookie.Expires = DateTime.Now.AddDays(1);
            //Response.Cookies["domain"].Domain = "support.contoso.com";
            return aCookie;
        }

        public bool VerifyCookie(HttpCookie aCookie)
        {
            var passwordHelper = new PasswordHelper();
            var userName = aCookie.Values["userName"];
            var hashKey = aCookie.Values["hashKey"];
            var cookieExpDate = aCookie.Values["exp"];

            if (passwordHelper.CheckIfPasswordMatch(userName + cookieExpDate.ToString(), hashKey))
            {
                return true;
            }

            return false;
        }

        public bool SignInByCookie(HttpCookie aCookie)
        {
            var cookieHelper = new CookieHelper();
            if (cookieHelper.VerifyCookie(aCookie))
            {
                var userName = aCookie.Values["userName"];
                FormsAuthentication.SetAuthCookie(userName, false);
                return true;
            }
            return false;
        }
    }
}