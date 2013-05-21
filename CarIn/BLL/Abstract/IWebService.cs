using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarIn.BLL.Abstract
{
    interface IWebService 
    {
        void MakeRequest();

        List<Type> GetResponse(Type T);


    }
}
