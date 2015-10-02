using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreForColman.Helpers
{
    public abstract class Currency
    {
        public double Value { get; set; }

        public Currency(double value)
        {
            this.Value = value;
        }

        public Currency(Currency currency)
        {
            this.Value = currency.Value;
        }
    }
}