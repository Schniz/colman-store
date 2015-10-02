using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreForColman.Helpers
{
    public class Shekel : Currency
    {
        public Shekel(double value) : base(value) { }
        public Shekel(Currency curr) : base(curr) { }

        public override string ToString()
        {
            return base.ToString() + "₪";
        }
    }
}