using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace StoreForColman.Services
{
    public class YoutubeUrlToMp4Link
    {
        string youtubeUrl;

        String RequestBody
        {
            get
            {
                NameValueCollection query = HttpUtility.ParseQueryString(String.Empty);
                query.Add("mediaurl", Uri.EscapeDataString(this.youtubeUrl));
                query.Add("filetype", "MP4");
                return query.ToString();
            }
        }

        public YoutubeUrlToMp4Link(string youtubeUrl)
        {
            this.youtubeUrl = youtubeUrl;
        }

        public string map()
        {
            Uri uri = new Uri("http://clipconverter.cc/check.php");
            WebRequest request = WebRequest.Create(uri);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            using (var stream = request.GetRequestStream())
            {
                byte[] requestData = Encoding.ASCII.GetBytes(this.RequestBody);
                stream.Write(requestData, 0, requestData.Length);
            }
            var response = request.GetResponse();
            var data = Json.Decode(new StreamReader(response.GetResponseStream()).ReadToEnd());
            return "";
        }
    }
}