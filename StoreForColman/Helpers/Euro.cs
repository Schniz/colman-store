using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreForColman.Helpers
{
    public class Euro : Currency
    {
        public Euro(double value) : base(value) { }
        public Euro(Currency curr) : base(curr) { }

        public override string ToString()
        {
            return base.ToString() + "€";
        }
    }
}