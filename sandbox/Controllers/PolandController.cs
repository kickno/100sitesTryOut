using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using sandbox.Models;

namespace sandbox.Controllers
{
    public class PolandController : Controller
    {
        //
        // GET: /Poland/
        /// <summary>
        /// todo: encode my email
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string lang = null)
        {
            ViewBag.Mail = "mailto:saki.jp";
            
            if(lang == null)
                lang = (Request.UserLanguages ?? Enumerable.Empty<string>()).FirstOrDefault();
            foreach (Poland p in _poland)
            {
                if (lang.Contains("ja"))
                {
                    p.Name = p.ID[0] + "j";
                }
                else
                {
                    p.Name = p.ID;
                }
            }

            if (Request.IsAjaxRequest())
            {
                
                return PartialView("_Diary", _poland);
            }
            else
            {
               
               return View(_poland);
            }
               
        }
        //public ActionResult ChangeLang(string lang = null)
        //{
           
        //}

        public static List<Poland> _poland =
            new List<Poland>
                {
                    new Poland
                        {
                            ID = "1",
                            Date = "Aug 31",
                            Color = "rgba(33,66,00,0.95)"
                        },
                    new Poland
                        {
                            ID = "2",
                            Date = "Sept 1",
                            Color = "rgba(66,99,00,0.95)"
                        },
                    new Poland
                        {
                            ID = "3",
                            Date = "Sept 2",
                            Color = "rgba(99,99,66,0.95)"
                        },
                    new Poland
                        {
                            ID = "4",
                            Date = "Sept 3",
                            Color = "rgba(255,204,33,0.95)"
                        },
                    new Poland
                        {
                            ID = "5",
                            Date = "Sept 4",
                            Color = "rgba(99,66,33,0.95)"
                        },
                    new Poland
                        {
                            ID = "6",
                            Date = "Sept 5",
                            Color = "rgba(99,66,66,0.95)"
                        },
                    new Poland
                        {
                            ID = "7",
                            Date = "Sept 6",
                            Color = "rgba(204,66,66,0.95)"
                        }
                };
    }
}
