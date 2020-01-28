using System;
using System.Collections.Generic;
using System.Text;

namespace CoinAPI.Consumer.Exceptions
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException() : base("Authentication failed, check your coinapi.io api key") { }
    }
}
