using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCart.WebServices
{
    public class CustomException : Exception
    {
        public CustomException(String errorMessageWithCode) : base(errorMessageWithCode)
        {
        }
    }
}
