namespace Webshop.Utils.ImageProvider
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Net;
    using System.Text;
    using System.IO;

    public class ImageCollector
    {
        public static string GetHtmlCode(string keyword)
        {
            string url = "https://www.google.com/search?q=" + keyword + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
             
             var response = (HttpWebResponse)request.GetResponse();

             using(Stream dataStream = response.GetResponseStream())
             {
                 if(dataStream == null)
                      return null;
                 using (var sr = new StreamReader(dataStream))
                 {
                     data = sr.ReadToEnd();
                 }
             }
             return data;
        }

        public static List<string> GetUrls(string html)
        {
            var urls = new List<string>();
            int ndx = html.IndexOf("\"ou\"", StringComparison.Ordinal);

            while (ndx >= 0)
            {
              ndx = html.IndexOf("\"", ndx + 4, StringComparison.Ordinal);
              ndx++;
              int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
              string url = html.Substring(ndx, ndx2 - ndx);
              urls.Add(url);
              ndx = html.IndexOf("\"ou\"", ndx2, StringComparison.Ordinal);
             }
             return urls;
        }
    }
}