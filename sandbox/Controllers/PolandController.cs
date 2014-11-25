using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sandbox.Controllers
{
    public class PolandController : Controller
    {
        //
        // GET: /Poland/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Poland/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Poland/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Poland/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Poland/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Poland/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Poland/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Poland/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
