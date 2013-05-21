using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarIn.BLL.Abstract
{
    interface IWebService<T> where T : class 
    {
        void MakeRequest();

        //void GetResponse(HttpWebRequest request);

        //void ParseResponse(object response);

        List<T> GetParsedResponse();

        void LogEvents(HttpStatusCode statusCode, string statusMessage);
    }
}
