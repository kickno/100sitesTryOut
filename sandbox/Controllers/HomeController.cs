using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace sandbox.Controllers
{
    using System.Xml;

    public class HomeController : Controller

    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hi, this is Top page of RIOT.JP";
            

            return View();
        }

        //private void GetPage(String url)
        //{
        //    try
        //    {
        //        Uri ourUri = new Uri(url);
        //        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(ourUri);
        //        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        //        for (int i = 0; i < myHttpWebResponse.Headers.Count; i++)
        //        {
        //            //Console.WriteLine("Header: {0}  Value: {1}", myHttpWebResponse.Headers.Keys[i].ToString(),
        //            //myHttpWebResponse.Headers.GetValues(i)[0].ToString());
        //        }
        //        myHttpWebResponse.Close();
        //    }
        //    catch (WebException e)
        //    {
        //        HttpWebResponse response = (HttpWebResponse)e.Response;
        //        if (response != null)
        //        {
        //            if (response.StatusCode == HttpStatusCode.Unauthorized)
        //            {
        //                string challenge = null;
        //                challenge = response.GetResponseHeader("WWW-Authenticate");
        //                if (challenge != null)
        //                    Console.WriteLine("\nThe following challenge was raised by the server:{0}", challenge);
        //            }
        //            else
        //                Console.WriteLine("\nThe following WebException was raised : {0}", e.Message);
        //        }
        //        else
        //            Console.WriteLine("\nResponse Received from server was null");

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("\nThe following Exception was raised : {0}", e.Message);
        //    }

        //}

        //private  List<KeyValuePair<string, string>> ReadXml()
        //{
        //    var xml = new List<KeyValuePair<string, string>>();
        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.Load(HttpContext.Server.MapPath("~/App_Data/somedata.xml"));
        //    foreach (XmlNode xn in xmldoc.SelectNodes("TOP/ENTRY"))
        //    {
        //        xml.Add(new KeyValuePair<string, string>(xn.SelectSingleNode("Name").InnerText, xn.SelectSingleNode("URL").InnerText));
        //    }
        //    return xml;

        //}
    }
}
