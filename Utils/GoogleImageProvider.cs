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
             string url = "https://www.google.nl/search?q="+ keyword + "&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjSoIrGkN_XAhWjLMAKHbXBB2MQ_AUICigB&biw=1536&bih=758";
             string data = "";

             var request = (HttpWebRequest)WebRequest.Create(url);
             var response = (HttpWebResponse)request.GetResponse();

             using (Stream dataStream = response.GetResponseStream())
             {
             if (dataStream == null)
                 return "";
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
    int ndx = html.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
    ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);

    while (ndx >= 0)
    {
        ndx = html.IndexOf("src=\"", ndx, StringComparison.Ordinal);
        ndx = ndx + 5;
        int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
        string url = html.Substring(ndx, ndx2 - ndx);
        urls.Add(url);
        ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);
    }
    return urls;
}
    }
}