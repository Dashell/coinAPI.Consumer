using System;
using System.Collections.Generic;
using System.Text;

namespace CoinAPI.Consumer.Exceptions
{
    public class CoinNotFoundException : Exception
    {
        public CoinNotFoundException(string coinId) : base($"Coin not found for id : {coinId}")
        {

        }
    }
}
