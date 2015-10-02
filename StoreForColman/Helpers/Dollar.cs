using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreForColman.Helpers
{
    public class Dollar : Currency
    {
        public Dollar(double value) : base(value) { }
        public Dollar(Currency curr) : base(curr) { }

        public override string ToString()
        {
            return "$" + base.ToString();
        }
    }
}