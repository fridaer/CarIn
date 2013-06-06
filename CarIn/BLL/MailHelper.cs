using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SUI.Helpers
{
    public static class MailHelper
    {
        // SHARP
        private static  string _smtpClient = "213.115.225.28";

        // LOCAL
        private const string _papercut = "127.0.0.1";

        //Port
        private const int _portNr = 26;
        // Admin email
        private static List<string> _admins = new List<string>()
                                                  {
                                                      "Fredrikoberg85@gmail.com"

                                                  };

        // OBS! MALL!! EPOSTMEDDELANDE SOM GÅR TILL ADMINISTRATÖR!!
        public static bool SendEmail(string nameOfService, string message, DateTime errorOccured)
        {
            var txtMessage =
                "<h3>Meddelande ifrån CarIn server</h3>" +
                "<b>Time : </b>" + errorOccured.ToString()+ "<br/>" +
                "<b>Message : </b><br/>" + message + "<br/>";

            try
            {

                using (var client = new SmtpClient(_papercut, _portNr))
                {

                    using (var mail = new MailMessage())
                    {
                        mail.From = new MailAddress("server@carin.se");
                        foreach (var admin in _admins)
                        {
                            mail.To.Add(new MailAddress(admin));
                        }
                        mail.Subject = "Message from CarIn";
                        mail.Body = txtMessage;
                        mail.IsBodyHtml = true;
                        mail.BodyEncoding = Encoding.GetEncoding(1252);
                        

                        client.Send(mail);
                        client.Dispose();
                    }
                }
            

            return true;
            }

            catch(Exception e)
            {
                return false;
            }
        }

    }
}