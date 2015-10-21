using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Helpers;

namespace StoreForColman.Services
{
    public class CurrencyConversionService
    {
        public static string createRequest()
        {
            string url = "https://query.yahooapis.com/v1/public/yql";
            string urlParameters = "?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22USDILS%22%2C%20%22EURILS%22%2C%20%22GBPILS%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

            var request = WebRequest.Create(url + urlParameters);
            request.ContentType = "application/json";
            var response = request.GetResponse();
            using (Stream st = response.GetResponseStream())
            {
                if (st != null)
                {
                    using (StreamReader sr = new StreamReader(st))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            throw new Exception("WAT");
        }
        public static Dictionary<String, double> getData()
        {
            DynamicJsonArray jsonObject = Json.Decode(createRequest()).query.results.rate;
            Dictionary<String, double> dict = new Dictionary<string, double>();
            foreach (dynamic item in jsonObject)
            {
                dict[item.Name.Split('/')[0]] = double.Parse(item.Rate);
            }
            return dict;
        }
    }
}