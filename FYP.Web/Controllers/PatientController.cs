using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult SpecialistNearby()
        {
            return View();
        }

        public ActionResult Medicine()
        {
            return View();
        }

        public ActionResult AuthorizedUsers()
        {
            return View();
        }

        public ActionResult Records()
        {
            return View();
        }
    }
}