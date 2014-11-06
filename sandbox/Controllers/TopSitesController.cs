using System;
using System.Collections.Generic;
using System.Web.Mvc;
using sandbox.Models;
using System.Net;
using System.Xml;
using System.Web.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Threading;
using System.Xml.XPath;
using System.IO;

namespace sandbox.Controllers
{
    public class TopSitesController : Controller
    {   //Paging: http://devproconnections.com/aspnet-mvc/aspnet-mvc-paging-done-perfectly
        //Child Asynchronous method not supported!:
        //https://aspnetwebstack.codeplex.com/workitem/601
        //Infinite Scrolling in MVC: 
        //http://www.codeproject.com/Tips/677599/ASP-NET-MVC-Ajax-Infinite-Scroll
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            int blocksize = 5;
            var model = ReadXmlTitle(page, blocksize);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult SiteList(List<TopSiteNameOnly> model)
        {
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult InfinateScroll(int BlockNumber)
        {
            //////////////// THis line of code only for demo. Needs to be removed ///////////////
            ///System.Threading.Thread.Sleep(3000);
            ////////////////////////////////////////////////////////////////////////////////////////

            int BlockSize = 5;
            var sites = ReadXmlTitle(BlockNumber, BlockSize);

            JsonModel jsonModel = new JsonModel();
            jsonModel.NoMoreData = sites.Count < BlockSize;
            jsonModel.HTMLString = RenderPartialViewToString("SiteList", sites);

            return Json(jsonModel);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        private List<TopSiteNameOnly> ReadXmlTitle(int startpage, int BlockSize)
        {
            int page;
            if (startpage != 1)
                page = (startpage - 1) * BlockSize + 1;
            else
                page = startpage;

            //setting.Async = true;
            XmlDocument doc = new XmlDocument();
            int i = page;
            var topsites = new List<TopSiteNameOnly>();
            XPathDocument document = new XPathDocument(HostingEnvironment.MapPath("~/App_Data/top100sites.xml"));
            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator iterator = navigator.Select("TOP/ENTRY[position()>=" + i.ToString() + " and not(position()>" + (page + BlockSize).ToString() + ")]");

            //Reader.MoveToContent();

            while (iterator.MoveNext() && (i < page + BlockSize))
            {
                if (iterator.Current.NodeType == XPathNodeType.Element)
                {
                    if (iterator.Current.Name == "ENTRY")
                    {
                        XmlNode xn = doc.ReadNode(iterator.Current.ReadSubtree());
                        var kv = new TopSiteNameOnly
                        {
                            ID = xn.SelectSingleNode("Rank").InnerText,
                            Name = xn.SelectSingleNode("Name").InnerText
                        };
                        topsites.Add(kv);
                        i++;
                    }

                }

            }

            return topsites;
        }


        #region "Query Part"

        public ActionResult Detail(string id)
        {
            var model = ReadXml(id);
            return PartialView("_Tops", model);
        }
        private TopSite ReadXml(string id)
        {
            TopSite topsites = new TopSite();
            XmlNode xn = null;
            XmlDocument doc = new XmlDocument();
            XPathDocument document = new XPathDocument(HostingEnvironment.MapPath("~/App_Data/top100sites.xml"));
            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator xni = navigator.Select("TOP/ENTRY[Rank/text()='" + id + "']");
            while (xni.MoveNext())
            {
                if (xni.Current.NodeType == XPathNodeType.Element && xni.Current.Name == "ENTRY")
                {
                    xn = doc.ReadNode(xni.Current.ReadSubtree());
                }
            }
            var oneTopSite = CreateData(xn);
            return oneTopSite;
        }

        /// <summary>
        /// How to Auto Refresh Partial View in ASP.Net
        /// http://www.mindstick.com/Articles/74634c5e-1c0b-4cba-b7a9-198a99551fac/Auto%20Refresh%20Partial%20View%20in%20ASP%20NET%20MVC How to 
        /// </summary>
        /// <returns></returns>
        // [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 3)] // every 3 sec
        //public ActionResult  GetTopSite()
        //{
        //      private static async Task<List<TopSite>> CreateData(List<KeyValuePair<string, string>> readPair)

        private TopSite CreateData(XmlNode xn)
        {
            TopSite ts = new TopSite();
            ts.ID = xn.SelectSingleNode("Rank").InnerText;
            ts.Name = xn.SelectSingleNode("Name").InnerText;
            ts.URL = xn.SelectSingleNode("URL").InnerText;

            var headerArr = GetPage("http://" + xn.SelectSingleNode("URL").InnerText);
            foreach (var arr in headerArr)
            {
                if (arr.Key.ToLower() == "cache-control") ts.CacheControl = arr.Value;
                if (arr.Key.ToLower() == "expires") ts.Expires = arr.Value;
                if (arr.Key.ToLower() == "date") ts.Date = arr.Value;
                if (arr.Key.ToLower() == "vary") ts.Vary = arr.Value;
                if (arr.Key.ToLower() == "etag") ts.Etag = arr.Value;
            }

            return ts;
        }

        /// <summary>
        /// From Microsoft HttpWebRequest Page. http://msdn.microsoft.com/en-us/library/system.net.httpwebresponse.headers(v=vs.110).aspx
        /// </summary>
        /// <param name="url"></param>
        private List<KeyValuePair<string, string>> GetPage(String url)
        {

            var returnarr = new List<KeyValuePair<string, string>>();
            try
            {

                Uri ourUri = new Uri(url);
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(ourUri);
                var myHttpWebResponse = myHttpWebRequest.GetResponse();
                for (int i = 0; i < myHttpWebResponse.Headers.Count; i++)
                {
                    KeyValuePair<string, string> kv = new KeyValuePair<string, string>(
                        myHttpWebResponse.Headers.Keys[i], myHttpWebResponse.Headers.GetValues(i)[0]);
                    returnarr.Add(kv);
                }
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        challenge = response.GetResponseHeader("WWW-Authenticate");
                        if (challenge != null) Console.WriteLine("The following challenge was raised by the server:{0}", challenge);
                    }
                    else Console.WriteLine("The following WebException was raised : {0}", e.Message);
                }
                else Console.WriteLine("Response Received from server was null");

            }
            catch (Exception e)
            {
                Console.WriteLine("The following Exception was raised : {0}", e.Message);
            }

            return returnarr;

        }


        #endregion
    }
}